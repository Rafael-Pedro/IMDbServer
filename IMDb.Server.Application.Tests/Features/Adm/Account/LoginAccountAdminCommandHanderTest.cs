using Moq;
using Xunit;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Token;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Features.Adm.Account.Login;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Tests.Features.Adm.Account
{
    public class LoginAccountAdminCommandHanderTest
    {
        private readonly Mock<ITokenService> tokenServiceMock = new();
        private readonly Mock<IAdminRepository> adminRepositoryMock = new();
        private readonly Mock<ICryptographyService> cryptographyServiceMock = new();

        [Fact]
        public async Task Handle_WhenAdminIsNotNullAndPasswordCorrect_ShouldLoginSuccessfully()
        {
            //Arrange
            var username = "testName";

            var salt = Array.Empty<byte>();
            var passwordCryptograph = Array.Empty<byte>();

            var adm = new Admin
            {
                IsActive = true,
                PasswordHash = passwordCryptograph,
                PasswordHashSalt = salt
            };

            var request = new LoginAccountAdmCommand("testName", "testPassword");

            var handler = new LoginAccountAdmCommandHandler(adminRepositoryMock.Object, tokenServiceMock.Object, cryptographyServiceMock.Object);

            adminRepositoryMock.Setup(arm => arm.GetByUsername(username, It.IsAny<CancellationToken>())).ReturnsAsync(adm);

            cryptographyServiceMock.Setup(csm => csm.Compare(passwordCryptograph, salt, request.Password)).Returns(true);

            var token = "Successfully generated token";
            var refreshToken = "Successfully refreshed token";

            tokenServiceMock.Setup(tsm => tsm.GenerateToken(adm)).Returns(token);
            tokenServiceMock.Setup(tsm => tsm.GenerateRefreshToken()).Returns(refreshToken);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(response.IsSuccess);
            Assert.Empty(response.Errors);
            Assert.Equal(token, response.Value.Token);
            Assert.Equal(refreshToken, response.Value.RefreshToken);
        }

        [Fact]
        public async Task Handle_WhenAdminIsNull_ShouldFailLogin()
        {
            //Arrange
            var request = new LoginAccountAdmCommand("testName", "testPassword");

            var handler = new LoginAccountAdmCommandHandler(adminRepositoryMock.Object, tokenServiceMock.Object, cryptographyServiceMock.Object);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(response.IsSuccess);
            Assert.NotEmpty(response.Errors);
            Assert.IsType<ApplicationError>(response.Errors.First());
            Assert.Null(response.ValueOrDefault);
        }

        [Fact]
        public async Task Handle_WhenAdminIsInactive_ShouldFailLogin()
        {
            //Arrange
            var salt = Array.Empty<byte>();
            var passwordCryptograph = Array.Empty<byte>();

            var adm = new Admin
            {
                IsActive = false
            };

            var request = new LoginAccountAdmCommand("testName", "testPassword");

            var handler = new LoginAccountAdmCommandHandler(adminRepositoryMock.Object, tokenServiceMock.Object, cryptographyServiceMock.Object);

            adminRepositoryMock.Setup(arm => arm.GetByUsername("testName", It.IsAny<CancellationToken>())).ReturnsAsync(adm);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(response.IsSuccess);
            Assert.NotEmpty(response.Errors);
            Assert.IsType<ApplicationError>(response.Errors.First());
            Assert.Null(response.ValueOrDefault);
        }

        [Fact]
        public async Task Handle_WhenPasswordIsNotCorrect_ShouldFailLogin()
        {
            //Arrange
            var salt = Array.Empty<byte>();
            var passwordCryptograph = Array.Empty<byte>();

            var request = new LoginAccountAdmCommand("testName", "testPassword");

            var handler = new LoginAccountAdmCommandHandler(adminRepositoryMock.Object, tokenServiceMock.Object, cryptographyServiceMock.Object);

            cryptographyServiceMock.Setup(csm => csm.Compare(passwordCryptograph, salt, request.Password)).Returns(false);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(response.IsSuccess);
            Assert.NotEmpty(response.Errors);
            Assert.IsType<ApplicationError>(response.Errors.First());
            Assert.Null(response.ValueOrDefault);
        }
    }
}
