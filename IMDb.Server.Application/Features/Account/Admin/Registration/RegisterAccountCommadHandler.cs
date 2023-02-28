using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Extension;

namespace IMDb.Server.Application.Features.Account.Admin.Registration;

public class RegisterAccountCommadHandler : IRequestHandler<RegisterAccountCommand, Result<RegisterAccountCommandResponse>>
{
    private readonly IAdminRepository adminRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICryptographyService cryptographyService;

    public RegisterAccountCommadHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork, ICryptographyService cryptographyService, IAdminRepository adminRepository)
    {
        this.unitOfWork = unitOfWork;
        this.cryptographyService = cryptographyService;
        this.adminRepository = adminRepository;
    }

    public async Task<Result<RegisterAccountCommandResponse>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        if (await adminRepository.IsUniqueUsername(request.Username.ToLower(), cancellationToken))
            return Result.Fail(new ApplicationError("Username already used."));

        if (await adminRepository.IsUniqueEmail(request.Email.ToLower(), cancellationToken))
            return Result.Fail(new ApplicationError("Email already used."));


    }
}
