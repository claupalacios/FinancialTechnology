using AutoMapper;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories.Interfaces;

namespace FinancialTechnology.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
    }
}
