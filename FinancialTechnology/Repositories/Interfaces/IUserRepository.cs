using FinancialTechnology.Models;

namespace FinancialTechnology.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public int AddUser(User user);
        public User GetUserById(int userId);
    }
}
