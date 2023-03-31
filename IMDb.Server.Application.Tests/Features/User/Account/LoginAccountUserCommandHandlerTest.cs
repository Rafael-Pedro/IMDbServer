using Xunit;
using NSubstitute;
using IMDb.Server.Application.Services.Token;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.Features.User.Login;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Features.Adm.Account.Login;

namespace IMDb.Server.Application.Tests.Features.User.Account;

public class LoginAccountUserCommandHandlerTest
{
    private readonly LoginAccountUserCommandHandler sut;
    private readonly ITokenService tokenServiceMock = Substitute.For<ITokenService>();
    private readonly IUsersRepository usersRepositoryMock = Substitute.For<IUsersRepository>();
    private readonly ICryptographyService cryptographyServiceMock = Substitute.For<ICryptographyService>();

    public LoginAccountUserCommandHandlerTest()
        => sut = new(usersRepositoryMock, cryptographyServiceMock, tokenServiceMock);

    [Fact]
    public async Task Handle_WhenUserIsNotNullAndPasswordIsCorrect_ShouldLoginSuccessfully()
    {
        //Arrange
        var lowerUsername = "testName".ToLower();

        var salt = Array.Empty<byte>();
        var passwordCryptograph = Array.Empty<byte>();

        var user = new Users
        {
            IsActive = true,
            PasswordHash = passwordCryptograph,
            PasswordHashSalt = salt
        };

        var request = new LoginAccountUserCommand("testName", "testPassword");

        usersRepositoryMock.GetByName(lowerUsername, Arg.Any<CancellationToken>()).Returns(user);

        cryptographyServiceMock.Compare(passwordCryptograph, salt, request.Password).Returns(true);

        var token = "Successfully generated token";
        var refreshToken = "Successfully refreshed token";

        tokenServiceMock.GenerateToken(user).Returns(token);
        tokenServiceMock.GenerateRefreshToken().Returns(refreshToken);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public async Task Handle_WhenUserIsNull_ShouldFailLogin()
    {
        //Arrange
        var request = new LoginAccountUserCommand("testName", "testPassword");

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
        Assert.Null(response.ValueOrDefault);
    }

    [Fact]
    public async Task Handle_WhenUserIsInactive_ShouldFailLogin()
    {
        //Arrange
        var lowerUsername = "testName".ToLower();

        var salt = Array.Empty<byte>();
        var passwordCryptograph = Array.Empty<byte>();

        var user = new Users
        {
            IsActive = false
        };

        var request = new LoginAccountUserCommand("testName", "testPassword");

        usersRepositoryMock.GetByName(lowerUsername, Arg.Any<CancellationToken>()).Returns(user);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

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

        var adm = new Admin
        {
            IsActive = true,
            PasswordHash = passwordCryptograph,
            PasswordHashSalt = salt
        };

        var request = new LoginAccountUserCommand("testName", "testPassword");

        cryptographyServiceMock.Compare(passwordCryptograph, salt, request.Password).Returns(false);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
        Assert.Null(response.ValueOrDefault);
    }
}
