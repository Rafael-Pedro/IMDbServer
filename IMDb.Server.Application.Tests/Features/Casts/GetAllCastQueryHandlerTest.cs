using IMDb.Server.Application.Features.Casts.GetAllCast;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using NSubstitute;
using Xunit;

namespace IMDb.Server.Application.Tests.Features.Casts;

public class GetAllCastQueryHandlerTest
{
    private readonly GetAllCastQueryHandler sut;
    private readonly ICastRepository castRepository = Substitute.For<ICastRepository>();

    public GetAllCastQueryHandlerTest()
    => sut = new(castRepository);

    [Fact]
    public async Task Handle_WhenCastListIsNotNull_ShouldSucess()
    {
        var request = new GetAllCastQuery(1, 10, false);

        var castList = new List<Cast>
        {
            new Cast {Id = 1, Name = "Generic name", Description = "Generic description", DateBirth = new DateTime(1999,02,08) },
            new Cast {Id = 2, Name = "Generic name2", Description = "Generic description2", DateBirth =  new DateTime (1999,02,08)},
        };

        var expectedResponse = castList.Select(c => new GetAllCastQueryResponse(c.Id, c.Name, c.Description, c.DateBirth));

        castRepository.GetAll(Arg.Any<PaginatedQueryOptions>()).Returns(castList);

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(expectedResponse, response.Value, true);
    }

    [Fact]
    public async Task Handle_WhenCastListIsNull_ShouldReturnEmpty()
    {
        var request = new GetAllCastQuery(1, 10, false);

        castRepository.GetAll(Arg.Any<PaginatedQueryOptions>()).Returns(Enumerable.Empty<Cast>());

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Value);
    }
}
