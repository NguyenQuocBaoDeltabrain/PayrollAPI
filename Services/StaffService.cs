using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations;

namespace PayrollAPI.Services
{
    public interface IStaffsService
    {
        StaffResponse findById(int id);
        void Create(StaffRequest staff);
        void Update(int id, StaffRequest staff);
        void Remove(int id);
    }

    public class StaffService : IStaffsService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public StaffService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StaffResponse findById(int id)
        {
            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            StaffResponse response = _mapper.Map<StaffResponse>(staff);
            return response;
        }
        public void Create(StaffRequest dto)
        {
            bool isExist = _context.Staffs.Count(staff => staff.name == dto.name) > 0;
            if(isExist) throw new BadHttpRequestException("Staff Is Exist");
            if (dto.salary < 1000000)
            {
                throw new BadHttpRequestException("Salary Not < 1,000,000 VND");
            }
            else
            {
                Staff staff = _mapper.Map<Staff>(dto);
                _ = _context.Staffs.Add(staff);
                _ = _context.SaveChanges();
            }
        }
        public void Update(int id, StaffRequest dto)
        {
            if (dto.salary < 1000000)
            {
                throw new BadHttpRequestException("Salary Not < 1,000,000 VND");
            }
            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            _ = _mapper.Map(dto,staff);
            _ = _context.Staffs.Update(staff);
            _ = _context.SaveChanges();
        }
        public void Remove(int id)
        {

            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            _ = _context.Staffs.Remove(staff);
            _ = _context.SaveChanges();

        }
    }
}