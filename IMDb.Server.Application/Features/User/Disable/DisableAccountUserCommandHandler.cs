using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;

namespace IMDb.Server.Application.Features.User.Disable;

public class DisableAccountUserCommandHandler : IRequestHandler<DisableAccountUserCommand, Result>
{
    private readonly IUsersRepository usersRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserInfo userInfo;

    public DisableAccountUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork, IUserInfo userInfo)
    {
        this.usersRepository = usersRepository;
        this.unitOfWork = unitOfWork;
        this.userInfo = userInfo;
    }

    public async Task<Result> Handle(DisableAccountUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetById(userInfo.Id, cancellationToken);

        if (user is null)
            return Result.Fail(new ApplicationError("User doesn't existis."));

        if (user.IsActive is false)
            return Result.Fail(new ApplicationError("User already disabled."));

        user.IsActive = false;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
