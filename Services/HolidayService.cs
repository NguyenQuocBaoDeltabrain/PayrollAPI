using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations;
namespace PayrollAPI.Services
{
    public interface IHolidayService
    {
        HolidayResponse findById(int id);
        void Create(HolidayRequest staff);
        void Update(int id, HolidayRequest staff);
        void Remove(int id);
    }
    public class HolidayService : IHolidayService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public HolidayService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public HolidayResponse findById(int id)
        {
            Holiday holiday = _context.Holidays.Find(id) ?? throw new KeyNotFoundException("Holiday Not Found");
            HolidayResponse response = _mapper.Map<HolidayResponse>(holiday);
            return response;
        }
        public void Create(HolidayRequest dto)
        {
            //bool isExist = _context.Holidays.Count(holiday => holiday.name == dto.name) > 0;
            //if (isExist) throw new BadHttpRequestException("Holiday Is Exist");
            //if (dto.salary < 1000000)
            //{
            //    throw new BadHttpRequestException("Salary Not < 1,000,000 VND");
            //}
            //else
            //{
                Holiday holiday = _mapper.Map<Holiday>(dto);
                 _context.Holidays.Add(holiday);
                 _context.SaveChanges();
            //}
        }
        public void Update(int id, HolidayRequest dto)
        {
            //if (dto.salary < 1000000)
            //{
            //    throw new BadHttpRequestException("Salary Not < 1,000,000 VND");
            //}
            Holiday holiday = _context.Holidays.Find(id) ?? throw new KeyNotFoundException("Holiday Not Found");
            _mapper.Map(dto, holiday);
            _context.Holidays.Update(holiday);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {

            Holiday holiday = _context.Holidays.Find(id) ?? throw new KeyNotFoundException("Holiday Not Found");
            _context.Holidays.Remove(holiday);
            _context.SaveChanges();

        }
    }
}
