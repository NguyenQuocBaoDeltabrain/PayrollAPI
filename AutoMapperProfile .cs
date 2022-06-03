using AutoMapper;
using PayrollAPI.Models;
using PayrollAPI.Validations;
namespace PayrollAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            _ = CreateMap<StaffRequest, Staff>();
            _ = CreateMap<Staff, StaffResponse>();
            _ = CreateMap<SalaryRequest, Salary>();
            _ = CreateMap<Salary, SalaryResponse>();
            _ = CreateMap<OverTimeRequest, OverTime>();
            _ = CreateMap<OverTime, OvertimeResponse>();
        }
    }
}