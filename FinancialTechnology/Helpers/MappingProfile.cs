using AutoMapper;
using FinancialTechnology.Dtos;
using FinancialTechnology.Models;

namespace FinancialTechnology.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
