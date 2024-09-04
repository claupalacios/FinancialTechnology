using AutoMapper;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories.Interfaces;

namespace FinancialTechnology.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddAccount(Account account)
        {
            _context.Account.Add(account);
            _context.SaveChanges();
            return account.Id;
        }
        public decimal? DepositToAccount(int accountId, decimal amount)
        {
            var accountToDeposit = _context.Account.Find(accountId);
            if (accountToDeposit != null)
            {
                accountToDeposit.Balance += amount;
                _context.SaveChanges();

                return accountToDeposit.Balance;
            }

            return null;
        }

        public decimal? WithdrawFromAccount(int accountId, decimal amount)
        {
            var accountToWithdraw = _context.Account.Find(accountId);
            if (accountToWithdraw != null)
            {
                if (accountToWithdraw.Balance >= amount)
                {
                    accountToWithdraw.Balance -= amount;
                    _context.SaveChanges();
                    return accountToWithdraw.Balance;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public decimal? GetAccountBalance(int accountId)
        {
            var accountToGetBalance = _context.Account.Find(accountId);

            if (accountToGetBalance != null)
            {
                return accountToGetBalance.Balance;
            }

            return null;
        }
    }
}
