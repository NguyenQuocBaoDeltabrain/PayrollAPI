using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;
using SuperHeroAPI.Validations;
using AutoMapper;

namespace SuperHeroAPI.Services
{
    public interface IStaffsService
    {
        StaffResponse GetStaffByID(int id);
        void Create(StaffRequest staff);
        void Update(int id,StaffRequest staff);
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

        public StaffResponse GetStaffByID(int id)
        {
            if (_context.Staffs == null)
            {
                throw new KeyNotFoundException("Staff table not exist record");
            }
            Staff staff = _context.Staffs.Find(id) ?? throw new KeyNotFoundException("Staff Not Found");
            var response = _mapper.Map<StaffResponse>(staff);
            return response;
        }

        public void Remove(int id)
        {
            if (_context.Staffs == null)
            {
                throw new KeyNotFoundException("Staff table not exist record");
            }
            Staff staff = _context.Staffs.Find(id);
            if(staff == null)
            {
                throw new KeyNotFoundException("Staff Not Found");

            }

            _context.Staffs.Remove(staff);
            _context.SaveChanges();
            
        }
        
        public void Create(StaffRequest dto)
        {
            if (_context.Staffs == null)
            {
                throw new KeyNotFoundException("Staff table not exist record");
            }
            Staff staff = _mapper.Map<Staff>(dto);
            _context.Staffs.Add(staff);
           _context.SaveChanges();
        }

        public void Update(int id,StaffRequest dto)
        {
            if (_context.Staffs == null)
            {
                throw new KeyNotFoundException("Staff table not exist record");
            }
            Staff IsStaff = _context.Staffs.Find(id);
            if (IsStaff == null)
            {
                throw new KeyNotFoundException("Staff Not Found");
            }
            _mapper.Map(dto, IsStaff);
            _context.Staffs.Update(IsStaff);
            _context.SaveChanges();
        }
    }
}
