using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.MoviesManagement.DeleteMovies;

public class DeleteMoviesCommandHandler : IRequestHandler<DeleteMoviesCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMoviesRepository moviesRepository;

    public DeleteMoviesCommandHandler(IUnitOfWork unitOfWork, IMoviesRepository moviesRepository)
    {
        this.unitOfWork = unitOfWork;
        this.moviesRepository = moviesRepository;
    }

    public async Task<Result> Handle(DeleteMoviesCommand request, CancellationToken cancellationToken)
    {
        var movie = await moviesRepository.GetById(request.Id, cancellationToken);

        if (movie is null)
            return Result.Fail(new ApplicationError("Movie doesn't exists."));

        moviesRepository.Delete(movie);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
