using Moq;
using Xunit;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Application.Features.Adm.GetActiveUsers;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Tests.Features.Adm.GetActiveUsers;

public class GetActiveUsersQueryHandlerTest
{
    private readonly GetActiveUsersQueryHandler handler;
    private readonly Mock<IUsersRepository> usersRepositoryMock = new();

    public GetActiveUsersQueryHandlerTest() 
    => handler = new(usersRepositoryMock.Object);

    [Fact]
    public async Task Handle_WhenActiveUsersExist_ShouldReturnActiveUsers()
    {
        // Arrange
        var request = new GetActiveUsersQuery(1, 10, false);
        var activeUsers = new List<Users>
        {
            new Users { Id = 1, Username = "user1", Email = "user1@test.com" },
            new Users { Id = 2, Username = "user2", Email = "user2@test.com" },
        };
        
        var expectedResponses = activeUsers.Select(au => new GetActiveUsersQueryResponse(au.Id, au.Username, au.Email));

        usersRepositoryMock.Setup(ur => ur.GetAllActiveUsers(It.IsAny<PaginatedQueryOptions>())).Returns(activeUsers);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(expectedResponses, response.Value, true);
    }

    [Fact]
    public async Task Handle_WhenNoActiveUsersExist_ShouldReturnEmptyList()
    {
        // Arrange
        var request = new GetActiveUsersQuery(1, 10, false);
        usersRepositoryMock.Setup(ur => ur.GetAllActiveUsers(It.IsAny<PaginatedQueryOptions>())).Returns(Enumerable.Empty<Users>());

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Value);
    }
}

