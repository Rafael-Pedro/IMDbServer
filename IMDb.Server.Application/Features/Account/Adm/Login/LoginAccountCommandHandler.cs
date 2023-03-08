using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Services.Token;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, Result<LoginAccountCommandResponse>>
{
    private readonly IAdminRepository adminRepository;
    private readonly ITokenService tokenService;
    private readonly ICryptographyService cryptographyService;
    private readonly IUnitOfWork unitOfWork;

    public LoginAccountCommandHandler(IAdminRepository adminRepository, ITokenService tokenService, ICryptographyService cryptographyService, IUnitOfWork unitOfWork)
    {
        this.adminRepository = adminRepository;
        this.tokenService = tokenService;
        this.cryptographyService = cryptographyService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<LoginAccountCommandResponse>> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByUserName(request.Username, cancellationToken);

        if (admin is null)
            return Result.Fail(new ApplicationError("Invalid credentials"));

        if (cryptographyService.Compare(admin.PasswordHash, admin.PasswordHashSalt, request.Password) is false)
            return Result.Fail(new ApplicationError("Invalid credentials"));

        var token = tokenService.GenerateToken(admin);
        var refreshToken = tokenService.GenerateRefreshToken();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new LoginAccountCommandResponse(true, token!, refreshToken));
    }
}
