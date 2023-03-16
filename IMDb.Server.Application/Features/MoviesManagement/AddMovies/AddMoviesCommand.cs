using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.MoviesManagement.AddMovies;

public record AddMoviesCommand(string Name,
    string Description,
    DateTime ReleaseDate,
    IEnumerable<int> Genres,
    IEnumerable<int> CastActor,
    IEnumerable<int> CastDirector
) : IRequest<Result<AddMoviesCommandResponse>>;
