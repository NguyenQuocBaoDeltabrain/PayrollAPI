using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using PayrollAPI.Models;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;
using AutoMapper;

namespace PayrollAPI.Services
{
    public interface ISalaryService
    {
        List<SalaryResponse> Finds(string month);
        List<SalaryResponse> FindsByMonth(FindStaffAndOTRequest dto);
        int TotalWorkdaysInMonth(int month, int year);
        float TaxEachStaff(float totalSalary);
        float SalaryOTEachStaff(Staff staff, List<OverTime> overTimes, int month, int year);
        ActionResult<object> Create(SalaryRequest salary);
    }
    public class SalaryService : ISalaryService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public SalaryService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool DateBelongToTheMonth(DateTime value, DateTime start, DateTime end)
        {
            return (start <= value) && (value <= end);
        }
        public List<OverTime> FindsOT(int staffId,int month,int year)
        {
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
            return overTimesFilterWithMonth;
        }
        public List<SalaryResponse> Finds(string month)
        {
            List<Salary> salaries = _context.Salaries.Where(salary => salary.month == month).ToList();
            List<SalaryResponse> salariesRO = CovertRO(salaries);
            return salariesRO;
        }

        public List<SalaryResponse> CovertRO(List<Salary> salaries)
        {
            List<SalaryResponse> salariesRO = new();
            foreach (Salary salary in salaries)
            {
                SalaryResponse salaryRO = _mapper.Map<SalaryResponse>(salary);
                salariesRO.Add(salaryRO);
            }
            return salariesRO;
        }

        public List<SalaryResponse> FindsByMonth(FindStaffAndOTRequest dto)
        {
          
            List<Salary> salaries = _context.Salaries.Where(salary => salary.month == dto.month).ToList();
            List<SalaryResponse> salariesRO = CovertRO(salaries);
            return salariesRO;
        }
        public int NumberOfParticularDaysInMonth(int year, int month, int totalDays, DayOfWeek dayOfWeek)
        {
            DateTime startDate = new(year, month, 1);
            int answer = Enumerable.Range(1, totalDays)
                .Select(item => new DateTime(year, month, item))
                .Where(date => date.DayOfWeek == dayOfWeek)
                .Count();
            return answer;
        }
        public int TotalWorkdaysInMonth(int month, int year)
        {
            int totalDays = DateTime.DaysInMonth(year, month);
            int numberOfSundays = NumberOfParticularDaysInMonth(year, month, totalDays, DayOfWeek.Sunday);
            int numberOfSaturdays = NumberOfParticularDaysInMonth(year, month, totalDays, DayOfWeek.Saturday);
            int totalWorkdays = totalDays - numberOfSaturdays - numberOfSundays;
            return totalWorkdays;
        }
        public float SalaryOTEachStaff(Staff staff, List<OverTime> overTimes, int month, int year)
        {
            float totalSalaryOT = 0;
            int numberOfWorkingHoursPerDay = 8;
            int numberOfWorkingDaysInMonth = TotalWorkdaysInMonth(month, year);
            int totalWorkingHoursInMonth = numberOfWorkingDaysInMonth * numberOfWorkingHoursPerDay;
            float salaryPerHour = staff.salary / totalWorkingHoursInMonth;

            foreach (OverTime overTime in overTimes)
            {
                float hourOT = (float)(overTime.endAt - overTime.startAt).TotalHours;
                float salaryOT;
                bool isWeekend = false;
                string covertToString = overTime.startAt.ToString("yyyy-MM-dd");
                bool isHoliday = _context.Holidays.Where(x => x.feteday.Equals(covertToString)).Count() > 0;

                if (isHoliday)
                {
                        isHoliday = true;
                        salaryOT = (float)(hourOT * salaryPerHour * 3);
                        totalSalaryOT += salaryOT;
                }

                if ((overTime.startAt.DayOfWeek == DayOfWeek.Saturday || overTime.startAt.DayOfWeek == DayOfWeek.Sunday) && !isHoliday)
                {
                    isWeekend = true;
                    salaryOT = (float)(hourOT * salaryPerHour * 2);
                    totalSalaryOT += salaryOT;
                }

                if (!isHoliday && !isWeekend)
                {
                    salaryOT = (float)(hourOT * salaryPerHour * 1.5);
                    totalSalaryOT += salaryOT;
                }

                isWeekend = false;
                isHoliday = false;
                overTime.isSalaryCalculated = true;
            }
            return totalSalaryOT;
        }

        public float TaxEachStaff(float totalSalary)
        {
            float incomeTaxes = totalSalary - 11000000;
            float tax = 0;
            switch (incomeTaxes)
            {
                case <0:
                    tax = 0;
                    break;
                case < 5000000:
                    tax = (float)(incomeTaxes * 0.05);
                    break;
                case < 10000000:
                    tax = (float)((incomeTaxes * 0.1) - 250000);
                    break;
                case < 18000000:
                    tax = (float)((incomeTaxes * 0.15) - 750000);
                    break;
                case < 32000000:
                    tax = (float)((incomeTaxes * 0.20) - 1650000);
                    break;
                case < 52000000:
                    tax = (float)((incomeTaxes * 0.25) - 3250000);
                    break;
                case < 80000000:
                    tax = (float)((incomeTaxes * 0.30) - 5850000);
                    break;
                default:
                    tax = (float)((incomeTaxes * 0.35) - 9850000);
                    break;
            }
            return tax;
        }
        public bool IsValid(string month)
        {
            bool result = DateTime.TryParseExact(
               month,
               "yyyy-MM",
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out _
            );
            return result;
        }

        public bool IsRecordExist(string month, int staffId)
        {
            int count = _context.Salaries.Count(sl => sl.staff.id == staffId && sl.month == month);
            return count > 0;
        }

        public ActionResult<object> Create(SalaryRequest salary)
        {
            bool isValid = IsValid(salary.month);
            if (!isValid)
            {
                throw new BadHttpRequestException("Input Incorrect!");
            }
            int month = int.Parse(salary.month.Split('-')[1]);
            int year = int.Parse(salary.month.Split('-')[0]);
            double totalRecordCreatedSuccess = 0;
            double totalRecordUpdatedSuccess = 0;
            float salaryOT;
            float tax;
            using var transaction = _context.Database.BeginTransaction();
            List<Staff> staffs = _context.Staffs.ToList();

            try
            {
                foreach (Staff staff in staffs)
                {
                    List<OverTime> overTimes = FindsOT(staff.id,month,year);
                    salaryOT = SalaryOTEachStaff(staff, overTimes, month, year);
                    float salaryBasic = staff.salary;
                    float totalSalary = salaryBasic + salaryOT;
                    tax = TaxEachStaff(totalSalary);
                    float insurance = (float)(salaryBasic * 0.105);
                    float salaryReceived = totalSalary - tax - insurance;
                    bool isExist = IsRecordExist(salary.month, staff.id);

                    if (isExist)
                    {
                        Salary salaryOfStaff = _context.Salaries.Where(salary => salary.month == salary.month && salary.staff.id == staff.id).FirstOrDefault();
                        salaryOfStaff.tax = tax;
                        salaryOfStaff.salaryOT = salaryOT; 
                        salaryOfStaff.salaryReceived = salaryReceived;
                        totalRecordUpdatedSuccess++;
                    }
                    else
                    {
                        Salary salaryOfStaff = new() { month = salary.month, salaryBasic = salaryBasic, salaryOT = salaryOT, tax = tax, insurance = insurance, salaryReceived = salaryReceived, staff = staff };
                        _context.Salaries.Add(salaryOfStaff);
                        totalRecordCreatedSuccess++;
                    }
                    _context.SaveChanges();
                }
                transaction.Commit();
                object result = new
                {
                    Message = "Success",
                    Created = totalRecordCreatedSuccess,
                    Updated = totalRecordUpdatedSuccess
                };
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return new
                {
                    Message = "Error",
                };
            }
        }
    }
}
