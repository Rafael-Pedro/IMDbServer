using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Extension;
using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Application.Features.Casts.AddCast;

public class CastCommandHandler : IRequestHandler<AddCastCommand, Result<CastCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICastRepository castRepository;

    public CastCommandHandler(IUnitOfWork unitOfWork, ICastRepository castRepository)
    {
        this.unitOfWork = unitOfWork;
        this.castRepository = castRepository;
    }

    public async Task<Result<CastCommandResponse>> Handle(AddCastCommand request, CancellationToken cancellationToken)
    {
        var cast = request.Name;
        if (await castRepository.IsUniqueCast(cast, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Artist already exists."));

        var newCast = new Cast
        {
            Name = request.Name,
            DateBirth = request.BirthDate,
            Description = request.Description
        };

        await castRepository.Create(newCast, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CastCommandResponse(newCast.Id));
    }
}
