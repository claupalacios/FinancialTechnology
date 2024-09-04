using FinancialTechnology.Controllers;
using FinancialTechnology.Models;
using FinancialTechnology.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FinancialTechnology_Test.Repositories
{
    public class AccountControllerTest
    {
        AccountController _target;
        Mock<IAccountService> _mockAccountService;

        public AccountControllerTest()
        {
            _mockAccountService = new Mock<IAccountService>();
            _target = new AccountController(_mockAccountService.Object);
        }

        [Test]
        public void AddAccount_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            // Arrange
            var accountDto = CreateAccountDto();
            var response = new Response<int> { IsSuccess = true };

            _mockAccountService
                .Setup(m => m.AddAccount(accountDto))
                .Returns(response);

            // Act
            var result = _target.AddAccount(accountDto);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddAccount_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            // Arrange
            var accountDto = CreateAccountDto();
            var response = new Response<int> { IsSuccess = false };

            _mockAccountService
                .Setup(m => m.AddAccount(accountDto))
                .Returns(response);

            // Act
            var result = _target.AddAccount(accountDto);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void DepositToAccount_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            // Arrange
            var response = new Response<int> { IsSuccess = true };

            _mockAccountService
                .Setup(m => m.DepositToAccount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(response);

            // Act
            var result = _target.DepositToAccount(1, 100.00m);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void DepositToAccount_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            // Arrange
            var response = new Response<int> { IsSuccess = false };

            _mockAccountService
                .Setup(m => m.DepositToAccount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(response);

            // Act
            var result = _target.DepositToAccount(1, 100.00m);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void WithdrawFromAccount_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            // Arrange
            var response = new Response<int> { IsSuccess = true };

            _mockAccountService
                .Setup(m => m.WithdrawFromAccount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(response);

            // Act
            var result = _target.WithdrawFromAccount(1, 50.00m);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void WithdrawFromAccount_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            // Arrange
            var response = new Response<int> { IsSuccess = false };

            _mockAccountService
                .Setup(m => m.WithdrawFromAccount(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(response);

            // Act
            var result = _target.WithdrawFromAccount(1, 50.00m);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void GetAccountBalance_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            // Arrange
            var response = new Response<int> { IsSuccess = true, Data = 100 };

            _mockAccountService
                .Setup(m => m.GetAccountBalance(It.IsAny<int>()))
                .Returns(response);

            // Act
            var result = _target.GetAccountBalance(1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetAccountBalance_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            // Arrange
            var response = new Response<int> { IsSuccess = false };

            _mockAccountService
                .Setup(m => m.GetAccountBalance(It.IsAny<int>()))
                .Returns(response);

            // Act
            var result = _target.GetAccountBalance(1);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        private AccountDto CreateAccountDto()
        {
            return new AccountDto
            {
                AccountNumber = "123456",
                AccountType = "Savings",
                Balance = 1000.00m
            };
        }
    }
}
