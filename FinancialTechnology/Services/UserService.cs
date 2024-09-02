using AutoMapper;
using FinancialTechnology.Dtos;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories.Interfaces;
using FinancialTechnology.Services.Interfaces;

namespace FinancialTechnology.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Insert a new User in the database
        /// </summary>
        /// <param name="account">User to insert</param>
        /// <returns>User Id added</returns>
        public Response<int> AddUser(UserDto user)
        {
            var response = new Response<int>();

            try
            {
                _logger.LogInformation("UserService - Adding new user");

                var userToAdd = _mapper.Map<User>(user);
                var userIdAdded = _userRepository.AddUser(userToAdd);

                response.Data = userIdAdded;
                response.IsSuccess = true;
                response.Message = $"User {user.Name} was added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UserService - The following error occurs: {@ex}", ex);
            }

            return response;
        }
    }
}
