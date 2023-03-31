﻿using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Extension;
using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Application.Features.Adm.Account.Registration;

public class RegistrationAccountAdmCommandHandler : IRequestHandler<RegistrationAccountAdmCommand, Result<RegistrationAccountAdmCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IAdminRepository adminRepository;
    private readonly ICryptographyService cryptographyService;

    public RegistrationAccountAdmCommandHandler(IUnitOfWork unitOfWork, ICryptographyService cryptographyService, IAdminRepository adminRepository)
    {
        this.unitOfWork = unitOfWork;
        this.cryptographyService = cryptographyService;
        this.adminRepository = adminRepository;
    }

    public async Task<Result<RegistrationAccountAdmCommandResponse>> Handle(RegistrationAccountAdmCommand request, CancellationToken cancellationToken)
    {
        var lowerUsername = request.Username.ToLower();
        var lowerEmail = request.Email.ToLower();

        if (await adminRepository.IsUniqueUsername(lowerUsername, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Username already used."));

        if (await adminRepository.IsUniqueEmail(lowerEmail, cancellationToken) is false)
            return Result.Fail(new ApplicationError("Email already used."));

        var salt = cryptographyService.CreateSalt();
        var passwordCryptograph = cryptographyService.Hash(request.Password, salt);

        var adm = new Admin
        {
            Email = lowerEmail,
            Username = lowerUsername,
            PasswordHashSalt = salt,
            PasswordHash = passwordCryptograph
        };

        await adminRepository.Create(adm, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new RegistrationAccountAdmCommandResponse(adm.Id));
    }
}
