using FinancialTechnology.Models;

namespace FinancialTechnology.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public int AddAccount(Account account);
        public decimal? DepositToAccount(int accountId, decimal amount);
        public decimal? WithdrawFromAccount(int accountId, decimal amount);
        public decimal? GetAccountBalance(int accountId);
    }
}
