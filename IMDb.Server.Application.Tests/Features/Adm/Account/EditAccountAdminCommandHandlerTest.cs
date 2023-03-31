using Moq;
using Xunit;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.Features.Adm.Account.Edit;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Features.Adm.Account.Registration;

namespace IMDb.Server.Application.Tests.Features.Adm.Account;

public class EditAccountAdminCommandHandlerTest
{
    private readonly Mock<IUserInfo> userInfoMock = new();
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
    private readonly Mock<IAdminRepository> adminRepositoryMock = new();
    private readonly Mock<ICryptographyService> cryptographyServiceMock = new();

    [Fact]
    public async Task Handle_WhenAdminAndPasswordIsNotNullAndUsernameAndEmailAreUnique_ShouldEditSuccessfully()
    {
        //Arrange
        var lowerUsername = "testName".ToLower();
        var lowerEmail = "testEmail@gmail.com".ToLower();

        var salt = Array.Empty<byte>();
        var passwordCryptograph = Array.Empty<byte>();

        var adm = new Admin
        {
            Username = lowerUsername,
            Email = lowerEmail,
            PasswordHashSalt = salt,
            PasswordHash = passwordCryptograph
        };

        var request = new EditAccountAdminCommand("testName", "testEmail@gmail.com", "testPassword");

        var handler = new EditAccountAdminCommandHandler(userInfoMock.Object, unitOfWorkMock.Object, adminRepositoryMock.Object, cryptographyServiceMock.Object);

        userInfoMock.Setup(uim => uim.Id).Returns(1);

        adminRepositoryMock.Setup(arm => arm.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(adm);
        adminRepositoryMock.Setup(arm => arm.IsUniqueUsername(lowerUsername, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        adminRepositoryMock.Setup(arm => arm.IsUniqueEmail(lowerEmail, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        adminRepositoryMock.Verify(arm => arm.Update(adm));
        unitOfWorkMock.VerifyAll();
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public async Task Handle_WhenAdminIsNull_ShouldFailEdit()
    {
        //Arrange
        var request = new EditAccountAdminCommand("testName", "testEmail@gmail.com", "testPassword");

        var handler = new EditAccountAdminCommandHandler(userInfoMock.Object, unitOfWorkMock.Object, adminRepositoryMock.Object, cryptographyServiceMock.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenAdminIsNotNullAndUsernameIsNotUnique_ShouldFailEdit()
    {
        var lowerUsername = "testNamewr".ToLower();

        adminRepositoryMock.Setup(arm => arm.IsUniqueUsername(lowerUsername, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var request = new EditAccountAdminCommand("testNamewr", "", "");

        var handler = new EditAccountAdminCommandHandler(userInfoMock.Object, unitOfWorkMock.Object, adminRepositoryMock.Object, cryptographyServiceMock.Object);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenAdminIsNotNullUsernameUniqueAndEmailNotUnique_ShouldFailEdit()
    {
        var lowerEmail = "testEmailwr@test.com".ToLower();

        adminRepositoryMock.Setup(arm => arm.IsUniqueEmail(lowerEmail, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var request = new EditAccountAdminCommand("", "testEmailwr@test.com", "");

        var handler = new EditAccountAdminCommandHandler(userInfoMock.Object, unitOfWorkMock.Object, adminRepositoryMock.Object, cryptographyServiceMock.Object);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
