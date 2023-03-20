using MediatR;
using FluentResults;
using IMDb.Server.Application.Extension;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.UserInfo;

namespace IMDb.Server.Application.Features.Adm.Account.Disable;

public class DisableAccountAdmCommandHandler : IRequestHandler<DisableAccountAdmCommand, Result>
{
    private readonly IAdminRepository adminRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserInfo userInfo;

    public DisableAccountAdmCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IUserInfo userInfo)
    {
        this.adminRepository = adminRepository;
        this.unitOfWork = unitOfWork;
        this.userInfo = userInfo;
    }

    public async Task<Result> Handle(DisableAccountAdmCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(userInfo.Id, cancellationToken);

        if (admin is null)
            return Result.Fail(new ApplicationError("Invalid admin"));

        if (admin.IsActive is false)
            return Result.Fail(new ApplicationError("Admin already disabled"));

        admin.IsActive = false;

        adminRepository.Update(admin);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
