using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Features.MoviesManagement.GetMoviesById;

public class GetMoviesByIdQueryHandler : IRequestHandler<GetMoviesByIdQuery, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMoviesRepository moviesRepository;

    public GetMoviesByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<Result> Handle(GetMoviesByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
