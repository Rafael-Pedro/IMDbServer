using Moq;
using Xunit;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Features.Adm.Account.Disable;

namespace IMDb.Server.Application.Tests.Features.Adm.Account;

public class DisableAccountAdminCommandHandlerTest
{
    private readonly Mock<IUserInfo> userInfoMock = new();
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
    private readonly Mock<IAdminRepository> adminRepositoryMock = new();

    [Fact]
    public async Task Handle_WhenAdminIsNotNullAndActive_ShouldDisable()
    {
        //Arrange
        var adm = new Admin
        {
            IsActive = true
        };

        var request = new DisableAccountAdmCommand();

        var handler = new DisableAccountAdmCommandHandler(adminRepositoryMock.Object, unitOfWorkMock.Object, userInfoMock.Object);

        userInfoMock.Setup(uim => uim.Id).Returns(1);

        adminRepositoryMock.Setup(arm => arm.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(adm);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        adminRepositoryMock.Verify(arm => arm.Update(adm));
        unitOfWorkMock.VerifyAll();
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public async Task Handle_WhenAdminIsNull_ShouldFailDisable()
    {
        //Arrange
        var request = new DisableAccountAdmCommand();

        var handler = new DisableAccountAdmCommandHandler(adminRepositoryMock.Object, unitOfWorkMock.Object, userInfoMock.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenAdminIsNotNullAndInactive_ShouldFailDisable()
    {
        //Arrange
        var adm = new Admin
        {
            IsActive = false
        };

        var request = new DisableAccountAdmCommand();

        var handler = new DisableAccountAdmCommandHandler(adminRepositoryMock.Object, unitOfWorkMock.Object, userInfoMock.Object);

        userInfoMock.Setup(uim => uim.Id).Returns(1);

        adminRepositoryMock.Setup(arm => arm.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(adm);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
