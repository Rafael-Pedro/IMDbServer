using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.User.Registration;

public class RegistrationAccountUserCommandHandler : IRequestHandler<RegistrationAccountUserCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUsersRepository usersRepository;
    private readonly ICryptographyService cryptographyService;

    public RegistrationAccountUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository, ICryptographyService cryptographyService)
    {
        this.unitOfWork = unitOfWork;
        this.usersRepository = usersRepository;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result> Handle(RegistrationAccountUserCommand request, CancellationToken cancellationToken)
    {
        var lowerUsername = request.Username.ToLower();
        var lowerEmail = request.Email.ToLower();

        if (await usersRepository.IsUniqueUsername(lowerUsername, cancellationToken))
            return Result.Fail(new ApplicationError("User already exists."));

        if (await usersRepository.IsUniqueEmail(lowerEmail, cancellationToken))
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

        return Result.Ok();
    }
}
