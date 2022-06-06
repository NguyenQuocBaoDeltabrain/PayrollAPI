using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;

namespace PayrollAPI.Services
{
    public interface IHolidayService
    {
        HolidayResponse FindById(int id);
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
        public HolidayResponse FindById(int id)
        {
            Holiday holiday = _context.Holidays.Find(id) ?? throw new KeyNotFoundException("Holiday Not Found");
            HolidayResponse response = _mapper.Map<HolidayResponse>(holiday);
            return response;
        }
        public void Create(HolidayRequest dto)
        {
            bool isExist = _context.Holidays.Count(holiday => holiday.feteday.Equals(dto.feteday))> 0;
            if (isExist) throw new BadHttpRequestException("Holiday Is Exist");
            Holiday holiday = _mapper.Map<Holiday>(dto);
            _context.Holidays.Add(holiday);
            _context.SaveChanges();
        }
        public void Update(int id, HolidayRequest dto)
        {
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