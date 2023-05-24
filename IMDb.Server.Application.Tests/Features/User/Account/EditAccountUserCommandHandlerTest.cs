using Xunit;
using NSubstitute;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Features.User.Edit;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Application.Extension;

namespace IMDb.Server.Application.Tests.Features.User.Account;

public class EditAccountUserCommandHandlerTest
{
    private readonly EditAccountUserCommandHandler sut;
    private readonly IUserInfo userInfoMock = Substitute.For<IUserInfo>();
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IUsersRepository usersRepositoryMock = Substitute.For<IUsersRepository>();
    private readonly ICryptographyService cryptographyServiceMock = Substitute.For<ICryptographyService>();

    public EditAccountUserCommandHandlerTest()
    => sut = new(unitOfWorkMock, usersRepositoryMock, cryptographyServiceMock, userInfoMock);

    [Fact]
    public async Task Handle_WhenUserAndPasswordIsNotNullAndUsernameAndEmailAreUnique_ShouldEditSuccessfully()
    {
        //Arrange
        var username = "testName";
        var email = "testEmail@gmail.com";

        var salt = Array.Empty<byte>();
        var passwordCryptograph = Array.Empty<byte>();

        var user = new Users
        {
            Username = username,
            Email = email,
            PasswordHashSalt = salt,
            PasswordHash = passwordCryptograph
        };

        var request = new EditAccountUserCommand("testName", "testEmail@gmail.com", "testPassword");

        userInfoMock.Id.Returns(1);

        usersRepositoryMock.GetById(1, Arg.Any<CancellationToken>()).Returns(user);
        usersRepositoryMock.IsUniqueUsername(username, Arg.Any<CancellationToken>()).Returns(true);
        usersRepositoryMock.IsUniqueEmail(email, Arg.Any<CancellationToken>()).Returns(true);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        usersRepositoryMock.Received().Update(user);
        await unitOfWorkMock.Received().SaveChangesAsync(CancellationToken.None);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public async Task Handle_WhenUserIsNull_ShouldFailEdit()
    {
        //Arrange
        var request = new EditAccountUserCommand("testName", "testEmail@gmail.com", "testPassword");

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenUserIsNotNullAndUsernameIsNotUnique_ShouldFailEdit()
    {
      usersRepositoryMock.IsUniqueUsername("testNamewr", Arg.Any<CancellationToken>()).Returns(false);

        var request = new EditAccountUserCommand("testNamewr", "", "");

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenUserIsNotNullUsernameUniqueAndEmailNotUnique_ShouldFailEdit()
    {
        usersRepositoryMock.IsUniqueEmail("testEmailwr@test.com", Arg.Any<CancellationToken>()).Returns(false);

        var request = new EditAccountUserCommand("", "testEmailwr@test.com", "");

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
