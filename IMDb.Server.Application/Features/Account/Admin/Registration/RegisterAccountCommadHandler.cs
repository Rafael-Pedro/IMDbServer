using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Extension;

namespace IMDb.Server.Application.Features.Account.Admin.Registration;

public class RegisterAccountCommadHandler : IRequestHandler<RegisterAccountCommand, Result<RegisterAccountCommandResponse>>
{
    private readonly IUsersRepository usersRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICryptographyService cryptographyService;

    public RegisterAccountCommadHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork, ICryptographyService cryptographyService)
    {
        this.usersRepository = usersRepository;
        this.unitOfWork = unitOfWork;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result<RegisterAccountCommandResponse>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        if (await usersRepository.IsUniqueEmail(request.Login.ToLower(), cancellationToken) is false)
            return Result.Fail(new ApplicationError("Email already used!"));
        throw new NotImplementedException();
    }
}
