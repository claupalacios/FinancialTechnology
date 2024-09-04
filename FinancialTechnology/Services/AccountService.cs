using AutoMapper;
using FinancialTechnology.Dtos;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories.Interfaces;
using FinancialTechnology.Services.Interfaces;

namespace FinancialTechnology.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IMapper mapper, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Insert a new Account in the database
        /// </summary>
        /// <param name="account">Account to insert</param>
        /// <returns>Account Id added</returns>
        public Response<int> AddAccount(AccountDto account)
        {
            var response = new Response<int>();

            try
            {
                _logger.LogInformation("AccountService - Adding new account");

                if (!Enum.IsDefined(typeof(AccountType), account.AccountType))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid account type. The type must be 'Saving', or 'Checking'.";
                }

                var userAccount = _userRepository.GetUserById(account.OwnerId);

                if (userAccount == null)
                {
                    response.Message = $"Owner Id {account.OwnerId} is incorrect.";
                    return response;
                }

                var accountToAdd = _mapper.Map<Account>(account);
                accountToAdd.Owner = userAccount;

                var accountIdAdded = _accountRepository.AddAccount(accountToAdd);

                response.Data = accountIdAdded;
                response.IsSuccess = true;
                response.Message = $"Account for {accountToAdd.Owner.Name} was added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogInformation("AccountService - The following error occurs: {@ex}", ex);
            }

            return response;
        }

        /// <summary>
        /// Deposit a specific amount into the account
        /// </summary>
        /// <param name="account">Account to deposit</param>
        /// <returns>New account balance</returns>
        public Response<decimal> DepositToAccount(int accountId, decimal amount)
        {
            var response = new Response<decimal>();

            try
            {
                _logger.LogInformation("AccountService - Depositing to account");

                var newBalance = _accountRepository.DepositToAccount(accountId, amount);

                if (newBalance != null)
                {
                    response.Data = newBalance.Value;
                    response.IsSuccess = true;
                    response.Message = "Deposit was successfully made.";
                }
                else
                {
                    response.Message = $"Account: {accountId} could not be updated.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("AccountService - The following error occurs: {@ex}", ex);
            }

            return response;
        }

        /// <summary>
        /// Withdraw a specific amount from the account
        /// </summary>
        /// <param name="account">Account to deposit</param>
        /// <returns>Task</returns>
        public Response<decimal> WithdrawFromAccount(int accountId, decimal amount)
        {
            var response = new Response<decimal>();

            try
            {
                _logger.LogInformation("AccountService - Withdrawing from account");

                var newBalance = _accountRepository.WithdrawFromAccount(accountId, amount);

                if (newBalance != null)
                {
                    response.Data = newBalance.Value;
                    response.IsSuccess = true;
                    response.Message = "Withdraw was successfully made.";
                }
                else
                {
                    response.Message = $"Could not withdraw from account: {accountId}.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("AccountService - The following error occurs: {@ex}", ex);
            }

            return response;
        }

        /// <summary>
        ///  Get the balance of a given account
        /// </summary>
        /// <param name="accountId">Id of the account</param>
        /// <returns>Balance of the account</returns>
        public Response<decimal> GetAccountBalance(int accountId)
        {
            var response = new Response<decimal>();

            try
            {
                _logger.LogInformation("TaskService - Getting account balance");
                var result = _accountRepository.GetAccountBalance(accountId);
                if (result != null)
                {
                    response.Data = result.Value;
                    response.IsSuccess = true;
                    response.Message = $"Account balance for account Id: {accountId} retrieved successfully.";
                }
                else
                {
                    response.Message = $"Account balance for account Id: {accountId} could not be retrieved.";
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("TaskService - The following error occurs: {@ex}.", ex);
            }

            return response;
        }
    }
}
