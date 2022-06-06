using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;
using System.Linq;

namespace PayrollAPI.Services
{
    public interface IStaffService
    {
        StaffResponse Find(int? id,string? name,bool state);
        void Create(StaffRequest staff);
        void Update(int id, StaffRequest staff);
        void Remove(int id);
        void DisableActive(int id);
    }

    public class StaffService : IStaffService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public StaffService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StaffResponse Find(int? id,string? name,bool state)
        {
            Staff staff = _context.Staffs.Where((emp) => (emp.id == id || emp.name == name) && emp.isActive == state).FirstOrDefault() ?? throw new KeyNotFoundException("Staff Not Found");
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
                _context.Staffs.Add(staff);
                _context.SaveChanges();
            }
        }
        public void Update(int id, StaffRequest dto)
        {
            if (dto.salary < 1000000)
            {
                throw new BadHttpRequestException("Salary Not < 1,000,000 VND");
            }
            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            _mapper.Map(dto,staff);
            _context.Staffs.Update(staff);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {

            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            _context.Staffs.Remove(staff);
            _context.SaveChanges();
        }
        public void DisableActive(int id)
        {
            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            staff.isActive = false;
            _context.SaveChanges();
        }
    }
}