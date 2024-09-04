using AutoMapper;
using FinancialTechnology.Dtos;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories.Interfaces;
using FinancialTechnology.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialTechnology.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Log in to the APP
        /// </summary>
        /// <returns>Token authenticator</returns>
        public Response<string> Login()
        {
            var response = new Response<string>();

            try
            {
                _logger.LogInformation("UserService - Getting new Token");

                var token = GetTokenJWT();
                response.Data = token;
                response.IsSuccess = true;
                response.Message = $"Token retrieved successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UserService - The following error occurs: {@ex}", ex);
            }

            return response;
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

        private string GetTokenJWT()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
