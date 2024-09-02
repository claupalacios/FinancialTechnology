using FinancialTechnology.Dtos;
using FinancialTechnology.Models;

namespace FinancialTechnology.Services.Interfaces
{
    public interface IAccountService
    {
        public Response<int> AddAccount(AccountDto account);
        public Response<decimal> DepositToAccount(int accountId, decimal amount);
        public Response<decimal> WithdrawFromAccount(int accountId, decimal amount);
        public Response<decimal> GetAccountBalance(int accountId);
    }
}
