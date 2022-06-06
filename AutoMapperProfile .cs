using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations;
namespace PayrollAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StaffRequest, Staff>();
            CreateMap<Staff, StaffResponse>();
            CreateMap<SalaryRequest, Salary>();
            CreateMap<Salary, SalaryResponse>();
            CreateMap<OverTimeRequest, OverTime>();
            CreateMap<OverTime, OvertimeResponse>();
            CreateMap<HolidayRequest, Holiday>();
            CreateMap<Holiday, HolidayResponse>();
        }
    }
}