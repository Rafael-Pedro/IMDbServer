using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Features.Casts.RemoveCast;

public class RemoveCastCommandHandler : IRequestHandler<RemoveCastCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICastRepository castRepository;

    public RemoveCastCommandHandler(IUnitOfWork unitOfWork, ICastRepository castRepository)
    {
        this.unitOfWork = unitOfWork;
        this.castRepository = castRepository;
    }

    public async Task<Result> Handle(RemoveCastCommand request, CancellationToken cancellationToken)
    {
        var cast = await castRepository.GetById(request.Id, cancellationToken);

        if (cast is null)
            return Result.Fail(new ApplicationError("Artist doesn't exists."));

        castRepository.Delete(cast);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
