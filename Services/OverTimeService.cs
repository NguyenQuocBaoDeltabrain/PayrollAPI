using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;

namespace PayrollAPI.Services
{
    public interface IOverTimeService
    {
        void Create(OverTimeRequest dto);
        void Remove(int id);
        List<OvertimeResponse> FinsByStaffIdAndMonth(int staffId, SalaryRequest salary);
    }
    public class OverTimeService : IOverTimeService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public OverTimeService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Create(OverTimeRequest dto)
        {
            bool isExist = _context.OverTimes.Count(ot => ot.staffId == dto.staffId && ot.startAt == dto.startAt && ot.endAt == ot.endAt) > 0;
            if (isExist) throw new BadHttpRequestException("Overtime Is Exist");
            bool isStaffExist = _context.Staffs.Count(staff => staff.id == dto.staffId) >0;
            if(!isStaffExist) throw new KeyNotFoundException("Staff ID Isn't Exist");
            OverTime overTime = _mapper.Map<OverTime>(dto);
            _context.OverTimes.Add(overTime);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {
            OverTime overTime = _context.OverTimes.Find(id) ?? throw new KeyNotFoundException("OverTime Not Found");
            _context.OverTimes.Remove(overTime);
            _context.SaveChanges();
        }
        public List<OvertimeResponse> ConvertToRO(List<OverTime> overtimes)
        {
            List<OvertimeResponse> overtimesRO = new();
            foreach (OverTime item in overtimes)
            {
                OvertimeResponse overtimeRO = _mapper.Map<OvertimeResponse>(item);
                overtimesRO.Add(overtimeRO);
            }
            return overtimesRO;
        }
        public bool DateBelongToTheMonth(DateTime value, DateTime start, DateTime end)
        {
            return (start <= value) && (value <= end);
        }
        public List<OvertimeResponse> FinsByStaffIdAndMonth(int staffId, SalaryRequest salary)
        {
            int month = int.Parse(salary.month.Split('-')[1]);
            int year = int.Parse(salary.month.Split('-')[0]);
            DateTime firstDayOfMonth = new(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<OverTime> overTimes = _context.OverTimes.Where(ot => ot.staffId == staffId).ToList();
            List<OverTime> overTimesFilterWithMonth = new();

            foreach (OverTime overTime in overTimes)
            {
                bool hasStartAtBelongToTheMonth = DateBelongToTheMonth(overTime.startAt, firstDayOfMonth, lastDayOfMonth);
                bool hasEndAtBelongToTheMonth = DateBelongToTheMonth(overTime.endAt, firstDayOfMonth, lastDayOfMonth);
                if (hasStartAtBelongToTheMonth && hasEndAtBelongToTheMonth)
                {
                    overTimesFilterWithMonth.Add(overTime);
                }
            }
            List<OvertimeResponse> overTimesRO = ConvertToRO(overTimesFilterWithMonth);
            return overTimesRO;
        }
    }
}
