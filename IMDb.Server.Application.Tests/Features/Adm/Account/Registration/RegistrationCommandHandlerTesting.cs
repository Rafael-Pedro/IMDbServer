using IMDb.Server.Application.Features.Adm.Account.Registration;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Moq;
using System;
using Xunit;

namespace IMDb.Server.Application.Tests.Features.Adm.Account.Registration;

public class RegistrationCommandHandlerTesting
{
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
    private readonly Mock<IAdminRepository> adminRepositoryMock = new();
    private readonly Mock<ICryptographyService> cryptoGraphyServiceMock = new();

    [Fact]
    public async Task Resgister_Sucess()
    {
        var lowerEmail = "testEmail@test.com".ToLower();
        var lowerUsername = "testName".ToLower();

        adminRepositoryMock.Setup(arm => arm.IsUniqueUsername(lowerUsername, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        adminRepositoryMock.Setup(arm => arm.IsUniqueEmail(lowerEmail, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var salt = Array.Empty<byte>();

        cryptoGraphyServiceMock.Setup(cs => cs.CreateSalt()).Returns(salt);

        var passwordCryptograph = Array.Empty<byte>();

        cryptoGraphyServiceMock.Setup(cs => cs.Hash("testPassword", salt)).Returns(passwordCryptograph);

        var adm = new Admin
        {
            Email = lowerEmail,
            Username = lowerUsername,
            PasswordHashSalt = salt,
            PasswordHash = passwordCryptograph
        };

        Admin actualAdm = null!;

        adminRepositoryMock.Setup(arm => arm.Create(It.IsAny<Admin>(), It.IsAny<CancellationToken>())).Callback((Admin a, CancellationToken ct) => actualAdm = a).Returns(Task.CompletedTask);

        var request = new RegistrationAccountAdmCommand("testName", "testEmail@test.com", "testPassword");

        var handler = new RegistrationAccountAdmCommandHandler(unitOfWorkMock.Object, cryptoGraphyServiceMock.Object, adminRepositoryMock.Object);

        var response = await handler.Handle(request, CancellationToken.None);

        unitOfWorkMock.VerifyAll();

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(adm, actualAdm, true);

    }
}

