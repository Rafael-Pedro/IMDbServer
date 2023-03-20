using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Token;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;
using IMDb.Server.Application.UserInfo;

namespace IMDb.Server.Application.Features.User.Login;
public class LoginAccountUserCommandHandler : IRequestHandler<LoginAccountUserCommand, Result<LoginAccountUserResponse>>
{
    private readonly IUsersRepository usersRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly ITokenService tokenService;

    public LoginAccountUserCommandHandler(IUsersRepository usersRepository, ICryptographyService cryptographyService, ITokenService tokenService)
    {
        this.usersRepository = usersRepository;
        this.cryptographyService = cryptographyService;
        this.tokenService = tokenService;
    }

    public async Task<Result<LoginAccountUserResponse>> Handle(LoginAccountUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByName(request.Username, cancellationToken);

        if (user is null)
            return Result.Fail(new ApplicationError("User doesn't exists"));

        if (cryptographyService.Compare(user.PasswordHash, user.PasswordHashSalt, request.Password) is false)
            return Result.Fail(new ApplicationError("Invalid credentials"));

        var token = tokenService.GenerateToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();

        return Result.Ok(new LoginAccountUserResponse(true, token!, refreshToken));
    }
}
