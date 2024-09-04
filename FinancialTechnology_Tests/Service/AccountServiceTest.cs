using AutoMapper;
using FinancialTechnology.Dtos;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories.Interfaces;
using FinancialTechnology.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FinancialTechnology_Test.Services
{
    public class AccountServiceTest
    {
        private AccountService _target;
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ILogger<AccountService>> _mockLogger;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<AccountService>>();
            SetupMapper();
            _target = new AccountService(_mockAccountRepository.Object, _mockUserRepository.Object, _mapper, _mockLogger.Object);
        }

        private void SetupMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountDto, Account>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void AddAccount_ShouldReturnIsSuccessTrue_When_AccountIsAddedSuccessfully()
        {
            // Arrange
            var accountDto = CreateAccountDto(AccountType.Savings);
            var user = CreateUser();
            _mockUserRepository.Setup(x => x.GetUserById(accountDto.OwnerId)).Returns(user);
            _mockAccountRepository.Setup(x => x.AddAccount(It.IsAny<Account>())).Returns(1);

            // Act
            var result = _target.AddAccount(accountDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Data);
        }

        [Test]
        public void AddAccount_ShouldReturnIsSuccessFalse_When_OwnerDoesNotExist()
        {
            // Arrange
            var accountDto = CreateAccountDto(AccountType.Savings);
            _mockUserRepository.Setup(x => x.GetUserById(accountDto.OwnerId)).Returns((User)null);

            // Act
            var result = _target.AddAccount(accountDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void AddAccount_ShouldReturnIsSuccessFalse_When_AccountTypeIsInvalid()
        {
            // Arrange
            var accountDto = CreateAccountDto((AccountType)500);

            // Act
            var result = _target.AddAccount(accountDto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void DepositToAccount_ShouldReturnIsSuccessTrue_When_DepositIsSuccessful()
        {
            // Arrange
            var accountId = 1;
            var amount = 100.00m;
            var returnData = 1100.00m;
            _mockAccountRepository.Setup(x => x.DepositToAccount(accountId, amount)).Returns(returnData);

            // Act
            var result = _target.DepositToAccount(accountId, amount);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(returnData, result.Data);
        }

        [Test]
        public void DepositToAccount_ShouldReturnIsSuccessFalse_When_DepositFails()
        {
            // Arrange
            var accountId = 1;
            var amount = 100.00m;
            _mockAccountRepository.Setup(x => x.DepositToAccount(accountId, amount)).Returns((decimal?)null);

            // Act
            var result = _target.DepositToAccount(accountId, amount);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void WithdrawFromAccount_ShouldReturnIsSuccessTrue_When_WithdrawIsSuccessful()
        {
            // Arrange
            var accountId = 1;
            var amount = 50.00m;
            var returnData = 950.00m;
            _mockAccountRepository.Setup(x => x.WithdrawFromAccount(accountId, amount)).Returns(returnData);

            // Act
            var result = _target.WithdrawFromAccount(accountId, amount);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(returnData, result.Data);
        }

        [Test]
        public void WithdrawFromAccount_ShouldReturnIsSuccessFalse_When_WithdrawFails()
        {
            // Arrange
            var accountId = 1;
            var amount = 50.00m;
            _mockAccountRepository.Setup(x => x.WithdrawFromAccount(accountId, amount)).Returns((decimal?)null);

            // Act
            var result = _target.WithdrawFromAccount(accountId, amount);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void GetAccountBalance_ShouldReturnIsSuccessTrue_When_BalanceIsRetrievedSuccessfully()
        {
            // Arrange
            var accountId = 1;
            var returnData = 1000.00m;
            _mockAccountRepository.Setup(x => x.GetAccountBalance(accountId)).Returns(returnData);

            // Act
            var result = _target.GetAccountBalance(accountId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(returnData, result.Data);
        }

        [Test]
        public void GetAccountBalance_ShouldReturnIsSuccessFalse_When_BalanceCannotBeRetrieved()
        {
            // Arrange
            var accountId = 1;
            _mockAccountRepository.Setup(x => x.GetAccountBalance(accountId)).Returns((decimal?)null);

            // Act
            var result = _target.GetAccountBalance(accountId);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        private AccountDto CreateAccountDto(AccountType accountType)
        {
            return new AccountDto
            {
                AccountNumber = 1234567890,
                AccountType = accountType,
                Balance = 1000.00m,
                OwnerId = 1
            };
        }

        private User CreateUser()
        {
            return new User
            {
                Id = 1,
                Name = "Puli Puccio"
            };
        }
    }
}
