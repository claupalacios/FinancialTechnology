using FinancialTechnology.Dtos;
using FinancialTechnology.Models;

namespace FinancialTechnology.Services.Interfaces
{
    public interface IUserService
    {
        public Response<int> AddUser(UserDto user);
    }
}
