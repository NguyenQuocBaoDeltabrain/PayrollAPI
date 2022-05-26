using AutoMapper;
using SuperHeroAPI.Models;
using SuperHeroAPI.Validations;
namespace SuperHeroAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Staff, StaffResponse>();
            CreateMap<StaffRequest, Staff>();
            CreateMap<Salary, SalaryResponse>();
            CreateMap<SalaryRequest, Salary>();

        }
    }
}
