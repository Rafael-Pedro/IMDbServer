using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.GenresManagment.AddGenre;

public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, Result<AddGenreCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IGenresRepository genresRepository;

    public AddGenreCommandHandler(IUnitOfWork unitOfWork, IGenresRepository genresRepository)
    {
        this.unitOfWork = unitOfWork;
        this.genresRepository = genresRepository;
    }

    public async Task<Result<AddGenreCommandResponse>> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = request.Genre;
        if (await genresRepository.IsUniqueGenre(genre, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Genre already exists."));

        var newGenre = new Genres()
        {
            Name = genre
        };

        await genresRepository.Create(newGenre, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new AddGenreCommandResponse(newGenre.Id));
    }
}
