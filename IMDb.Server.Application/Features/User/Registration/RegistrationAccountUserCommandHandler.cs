using MediatR;
using FluentResults;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Features.User.Registration;

public class RegistrationAccountUserCommandHandler : IRequestHandler<RegistrationAccountUserCommand, Result<RegistrationAccountUserCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUsersRepository usersRepository;
    private readonly ICryptographyService cryptographyService;

    public RegistrationAccountUserCommandHandler(IUnitOfWork unitOfWork, ICryptographyService cryptographyService, IUsersRepository usersRepository)
    {
        this.unitOfWork = unitOfWork;
        this.cryptographyService = cryptographyService;
        this.usersRepository = usersRepository;
    }

    public async Task<Result<RegistrationAccountUserCommandResponse>> Handle(RegistrationAccountUserCommand request, CancellationToken cancellationToken)
    {
        var lowerUsername = request.Username.ToLower();
        var lowerEmail = request.Email.ToLower();

        if (await usersRepository.IsUniqueUsername(lowerUsername, cancellationToken) is false)
            return Result.Fail(new ApplicationError("User already exists."));

        if (await usersRepository.IsUniqueEmail(lowerEmail, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Email already used."));

        var salt = cryptographyService.CreateSalt();
        var passwordHash = cryptographyService.Hash(request.Password, salt);

        var user = new Users
        {
            Username = lowerUsername,
            Email = lowerEmail,
            PasswordHash = passwordHash,
            PasswordHashSalt = salt
        };

        await usersRepository.Create(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new RegistrationAccountUserCommandResponse(user.Id));
    }
}
