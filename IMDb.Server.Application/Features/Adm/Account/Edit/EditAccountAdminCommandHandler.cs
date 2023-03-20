using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;
using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Application.Features.Adm.Account.Edit;
public class EditAccountAdminCommandHandler : IRequestHandler<EditAccountAdminCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IAdminRepository adminRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly IUserInfo userInfo;

    public EditAccountAdminCommandHandler(IUnitOfWork unitOfWork, IAdminRepository adminRepository, ICryptographyService cryptographyService, IUserInfo userInfo)
    {
        this.unitOfWork = unitOfWork;
        this.adminRepository = adminRepository;
        this.cryptographyService = cryptographyService;
        this.userInfo = userInfo;
    }

    public async Task<Result> Handle(EditAccountAdminCommand request, CancellationToken cancellationToken)
    {
        var adm = await adminRepository.GetById(userInfo.Id, cancellationToken);

        var lowerUsername = request.Username.ToLower();
        var lowerEmail = request.Email.ToLower();

        if (adm is null)
            return Result.Fail(new ApplicationError("User doesn't exists."));

        if (await adminRepository.IsUniqueUsername(lowerUsername, cancellationToken))
            return Result.Fail(new ApplicationError("Username already used."));

        if (await adminRepository.IsUniqueEmail(lowerEmail, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Email aleaready used."));

        if (request.Password is not null)
        {
            var salt = cryptographyService.CreateSalt();
            var passwordCrypt = cryptographyService.Hash(request.Password, salt);
            adm.PasswordHashSalt = salt;
            adm.PasswordHash = passwordCrypt;
        }

        adm.Username = lowerUsername;
        adm.Email = lowerEmail;

        adminRepository.Update(adm);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
