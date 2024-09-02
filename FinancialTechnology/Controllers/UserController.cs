using FinancialTechnology.Dtos;
using FinancialTechnology.Models;
using FinancialTechnology.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTechnology.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add-user")]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [ProducesResponseType(400)]
        public IActionResult AddUser(UserDto account)
        {
            var response = _userService.AddUser(account);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
