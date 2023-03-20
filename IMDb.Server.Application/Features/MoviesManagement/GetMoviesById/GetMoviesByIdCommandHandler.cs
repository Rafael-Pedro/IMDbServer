using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Features.MoviesManagement.GetMoviesById;

public class GetMoviesByIdCommandHandler : IRequestHandler<GetMoviesByIdCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMoviesRepository moviesRepository;

    public GetMoviesByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<Result> Handle(GetMoviesByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
