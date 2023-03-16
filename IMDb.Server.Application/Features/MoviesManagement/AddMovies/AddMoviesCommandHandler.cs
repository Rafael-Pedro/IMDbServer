using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.MoviesManagement.AddMovies;

public class AddMoviesCommandHandler : IRequestHandler<AddMoviesCommand, Result<AddMoviesCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMoviesRepository moviesRepository;
    private readonly IGenresRepository genresRepository;
    private readonly ICastRepository castRepository;
    public AddMoviesCommandHandler(IUnitOfWork unitOfWork, IMoviesRepository moviesRepository, IGenresRepository genresRepository, ICastRepository castRepository)
    {
        this.unitOfWork = unitOfWork;
        this.moviesRepository = moviesRepository;
        this.genresRepository = genresRepository;
        this.castRepository = castRepository;
    }

    public async Task<Result<AddMoviesCommandResponse>> Handle(AddMoviesCommand request, CancellationToken cancellationToken)
    {

        if(await moviesRepository.IsUniqueName(request.Name.ToLower(), cancellationToken))
            return Result.Fail(new ApplicationError("Movie already registred."));

        if (await genresRepository.ExistingGenders(request.Genres, cancellationToken))
            return Result.Fail(new ApplicationError("Some genre is invalid."));

        if (await castRepository.IsAlreadyRegistred(request.CastActor, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Some actor doesn't exists."));

        if (await castRepository.IsAlreadyRegistred(request.CastDirector, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Some director doesn't exists."));

        var genresMovies = request.Genres.Select(genre => new GenresMovies { GenresId = genre });

        var actorCast = request.CastActor.Select(actor => new CastActMovies { CastActId = actor });

        var directorCast = request.CastDirector.Select(director => new CastDirectMovies { CastDirectorId = director });

        var movie = new Movies
        {
            Name = request.Name,
            Description = request.Description,
            ActorCast = actorCast,
            DirectorCast = directorCast,
            GenresMovies = genresMovies
        };

        await moviesRepository.Create(movie, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new AddMoviesCommandResponse(movie.Id));
    }
}
