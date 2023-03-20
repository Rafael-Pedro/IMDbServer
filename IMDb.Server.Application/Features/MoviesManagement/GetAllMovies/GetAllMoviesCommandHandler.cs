using FluentResults;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.MoviesManagement.GetAllMovies;

public class GetAllMoviesCommandHandler : IRequestHandler<GetAllMoviesCommand, Result>
{
    private readonly IMoviesRepository moviesRepository;

    public GetAllMoviesCommandHandler(IMoviesRepository moviesRepository)
    {
        this.moviesRepository = moviesRepository;
    }

    Task<Result> IRequestHandler<GetAllMoviesCommand, Result>.Handle(GetAllMoviesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
