using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Features.Casts.AddCast;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using NSubstitute;
using Xunit;

namespace IMDb.Server.Application.Tests.Features.Casts;

public class AddCastCommandHandlerTest
{
    private readonly CastCommandHandler sut;
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ICastRepository castRepository = Substitute.For<ICastRepository>();

    public AddCastCommandHandlerTest()
    => sut = new(unitOfWork, castRepository);

    [Fact]
    public async Task Handle_WhenCastIsUnique_ShouldRegisterCast()
    {
        var cast = "testCast";
        var dateBirth = new DateTime(1999, 02, 08);
        var description = "Lorem ipsum dolor sit amet.";

        var newCast = new Cast
        {
            Name = cast,
            DateBirth = dateBirth,
            Description = description
        };

        Cast actualCast = null!;

        var request = new AddCastCommand(cast, description, dateBirth);

        castRepository.IsUniqueCast(cast, Arg.Any<CancellationToken>()).Returns(true);
        castRepository.Create(Arg.Do<Cast>(c => actualCast = c), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var response = await sut.Handle(request, CancellationToken.None);

        unitOfWork.Received();

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(newCast, actualCast, true);
    }

    [Fact]
    public async Task Handle_WhenCastAlreadyExists_ShouldFail()
    {
        var genre = "testGenre";
        castRepository.IsUniqueCast(genre, Arg.Any<CancellationToken>()).Returns(false);

        var request = new AddCastCommand(genre, "", new DateTime());

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
