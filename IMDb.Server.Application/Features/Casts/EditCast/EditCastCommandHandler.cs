using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Extension;

namespace IMDb.Server.Application.Features.Casts.EditCast;

public class EditCastCommandHandler : IRequestHandler<EditCastCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICastRepository castRepository;

    public EditCastCommandHandler(IUnitOfWork unitOfWork, ICastRepository castRepository)
    {
        this.unitOfWork = unitOfWork;
        this.castRepository = castRepository;
    }

    public async Task<Result> Handle(EditCastCommand request, CancellationToken cancellationToken)
    {
        var cast = await castRepository.GetById(request.Id, cancellationToken);

        if (cast is null)
            return Result.Fail(new ApplicationError("Artist doesn't exists"));

        if (await castRepository.IsUniqueCast(request.Name, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Name already used"));

        cast.Name = request.Name ?? cast.Name;
        cast.Description = request.Description ?? cast.Description;
        cast.DateBirth = request.BirthDate ?? cast.DateBirth;

        castRepository.Update(cast);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
