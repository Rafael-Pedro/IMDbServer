using FluentResults;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.Account.User.Login;
public class LoginAccountUserCommandHandler : IRequestHandler<LoginAccountUserCommand, Result<LoginAccountUserResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUsersRepository usersRepository;
    private readonly ICryptographyService cryptographyService;

    public LoginAccountUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository, ICryptographyService cryptographyService)
    {
        this.unitOfWork = unitOfWork;
        this.usersRepository = usersRepository;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result<LoginAccountUserResponse>> Handle(LoginAccountUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByName(request.Username, cancellationToken);



        throw new NotImplementedException();
    }
}
