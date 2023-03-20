using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;

namespace IMDb.Server.Application.Features.User.Edit;

public class EditAccountUserCommandHandler : IRequestHandler<EditAccountUserCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUsersRepository usersRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly IUserInfo userInfo;

    public EditAccountUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository, ICryptographyService cryptographyService, IUserInfo userInfo)
    {
        this.unitOfWork = unitOfWork;
        this.usersRepository = usersRepository;
        this.cryptographyService = cryptographyService;
        this.userInfo = userInfo;
    }

    public async Task<Result> Handle(EditAccountUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetById(userInfo.Id, cancellationToken);

        var lowerUsername = request.Username.ToLower();
        var lowerEmail = request.Email.ToLower();

        if (user is null)
            return Result.Fail(new ApplicationError("User doesn't exists."));

        if (await usersRepository.IsUniqueUsername(lowerUsername, cancellationToken))
            return Result.Fail(new ApplicationError("Username already used."));

        if (await usersRepository.IsUniqueEmail(lowerEmail, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Email aleaready used."));

        if (request.Password is not null)
        {
            var salt = cryptographyService.CreateSalt();
            var passwordCrypt = cryptographyService.Hash(request.Password, salt);
            user.PasswordHash = passwordCrypt;
            user.PasswordHashSalt = salt;
        }

        user.Username = lowerUsername;
        user.Email = lowerEmail;

        usersRepository.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
