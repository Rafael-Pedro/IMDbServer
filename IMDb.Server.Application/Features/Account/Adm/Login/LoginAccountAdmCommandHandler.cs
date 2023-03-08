using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Services.Token;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public class LoginAccountAdmCommandHandler : IRequestHandler<LoginAccountAdmCommand, Result<LoginAccountAdmCommandResponse>>
{
    private readonly IAdminRepository adminRepository;
    private readonly ITokenService tokenService;
    private readonly ICryptographyService cryptographyService;

    public LoginAccountAdmCommandHandler(IAdminRepository adminRepository, ITokenService tokenService, ICryptographyService cryptographyService)
    {
        this.adminRepository = adminRepository;
        this.tokenService = tokenService;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result<LoginAccountAdmCommandResponse>> Handle(LoginAccountAdmCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByUserName(request.Username, cancellationToken);

        if (admin is null)
            return Result.Fail(new ApplicationError("Invalid credentials"));

        if (cryptographyService.Compare(admin.PasswordHash, admin.PasswordHashSalt, request.Password) is false)
            return Result.Fail(new ApplicationError("Invalid credentials"));

        var token = tokenService.GenerateToken(admin);
        var refreshToken = tokenService.GenerateRefreshToken();

        return Result.Ok(new LoginAccountAdmCommandResponse(true, token!, refreshToken));
    }
}
