using FinancialTechnology.Dtos;
using FinancialTechnology.Models;
using FinancialTechnology.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTechnology.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("add-account")]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [ProducesResponseType(400)]
        public IActionResult AddAccount(AccountDto account)
        {
            var response = _accountService.AddAccount(account);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPatch("deposit")]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [ProducesResponseType(400)]
        public IActionResult DepositToAccount(int accountId, decimal amount)
        {
            var response = _accountService.DepositToAccount(accountId, amount);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPatch("withdraw")]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [ProducesResponseType(400)]
        public IActionResult WithdrawFromAccount(int accountId, decimal amount)
        {
            var response = _accountService.WithdrawFromAccount(accountId, amount);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("balance")]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetAccountBalance(int accountId)
        {
            var response = _accountService.GetAccountBalance(accountId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
