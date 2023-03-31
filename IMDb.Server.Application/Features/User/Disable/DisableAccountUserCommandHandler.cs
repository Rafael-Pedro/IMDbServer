using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Features.User.Disable;

public class DisableAccountUserCommandHandler : IRequestHandler<DisableAccountUserCommand, Result>
{
    private readonly IUserInfo userInfo;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUsersRepository usersRepository;

    public DisableAccountUserCommandHandler(IUserInfo userInfo, IUnitOfWork unitOfWork, IUsersRepository usersRepository)
    {
        this.userInfo = userInfo;
        this.unitOfWork = unitOfWork;
        this.usersRepository = usersRepository;
    }

    public async Task<Result> Handle(DisableAccountUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetById(userInfo.Id, cancellationToken);

        if (user is null)
            return Result.Fail(new ApplicationError("User doesn't exist."));

        if (user.IsActive is false)
            return Result.Fail(new ApplicationError("User already disabled."));

        user.IsActive = false;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
