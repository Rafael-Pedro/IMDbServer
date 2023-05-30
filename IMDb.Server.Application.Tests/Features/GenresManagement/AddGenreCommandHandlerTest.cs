using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Features.GenresManagment.AddGenre;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using NSubstitute;
using Xunit;

namespace IMDb.Server.Application.Tests.Features.GenresManagement;

public class AddGenreCommandHandlerTest
{
    private readonly AddGenreCommandHandler sut;
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IGenresRepository genresRepository = Substitute.For<IGenresRepository>();

    public AddGenreCommandHandlerTest()
        => sut = new(unitOfWork, genresRepository);

    [Fact]
    public async Task Handle_WhenGenreDoesNotExists_ShouldCreateGenre()
    {
        //Arrange
        var genre = "genreTest";

        var newGenre = new Genres
        {
            Name = genre
        };

        Genres actualGenre = null!;

        var request = new AddGenreCommand("genreTest");

        genresRepository.IsUniqueGenre(genre, Arg.Any<CancellationToken>()).Returns(true);
        genresRepository.Create(Arg.Do<Genres>(g => actualGenre = g), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        unitOfWork.Received();

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(newGenre, actualGenre, true);
    }

    [Fact]
    public async Task Handle_WhenGenreAlreadyExists_ShouldFail()
    {

        genresRepository.IsUniqueGenre("genreTestwr", Arg.Any<CancellationToken>()).Returns(false);

        var request = new AddGenreCommand("genreTestwr");

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
