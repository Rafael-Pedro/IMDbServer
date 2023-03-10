using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Services.Cryptography;

namespace IMDb.Server.Application.Features.Account.User.Edit;

public class EditAccountUserCommandHandler : IRequestHandler<EditAccountUserCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUsersRepository usersRepository;
    private readonly ICryptographyService cryptographyService;

    public EditAccountUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository, ICryptographyService cryptographyService)
    {
        this.unitOfWork = unitOfWork;
        this.usersRepository = usersRepository;
        this.cryptographyService = cryptographyService;
    }

    public Task<Result> Handle(EditAccountUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
