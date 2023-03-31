using Xunit;
using NSubstitute;
using IMDb.Server.Application.Features.User.Disable;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Infra.Database.Abstraction;

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
    public async Task Handle_WhenAdminIsNotNullAndActive_ShouldDisable()
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


}
