using Xunit;
using NSubstitute;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Features.User.Disable;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Tests.Features.User.Account;

public class DisableAccountUserCommandHandlerTest
{
    private readonly DisableAccountUserCommandHandler sut;
    private readonly IUserInfo userInfoMock = Substitute.For<IUserInfo>();
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IUsersRepository usersRepositoryMock = Substitute.For<IUsersRepository>();

    public DisableAccountUserCommandHandlerTest()
    => sut = new(userInfoMock, unitOfWorkMock, usersRepositoryMock);

    [Fact]
    public async Task Handle_WhenUserIsNotNullAndActive_ShouldDisable()
    {
        //Arrange
        var user = new Users
        {
            IsActive = true
        };

        var request = new DisableAccountUserCommand();

        userInfoMock.Id.Returns(1);

        usersRepositoryMock.GetById(1, Arg.Any<CancellationToken>()).Returns(user);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        await unitOfWorkMock.Received().SaveChangesAsync(CancellationToken.None);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public async Task Handle_WhenUserIsNull_ShouldFailDisable()
    {
        //Arrange
        var request = new DisableAccountUserCommand();

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenUserIsNotNullAndInactive_ShouldFailDisable()
    {
        //Arrange
        var user = new Users
        {
            IsActive = false
        };

        var request = new DisableAccountUserCommand();

        userInfoMock.Id.Returns(1);

        usersRepositoryMock.GetById(1, Arg.Any<CancellationToken>()).Returns(user);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
