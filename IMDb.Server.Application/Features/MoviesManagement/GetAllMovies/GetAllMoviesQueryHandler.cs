using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Features.MoviesManagement.GetAllMovies;

public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, Result>
{
    private readonly IMoviesRepository moviesRepository;

    public GetAllMoviesQueryHandler(IMoviesRepository moviesRepository)
    {
        this.moviesRepository = moviesRepository;
    }

    Task<Result> IRequestHandler<GetAllMoviesQuery, Result>.Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
