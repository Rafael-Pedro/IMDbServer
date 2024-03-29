﻿using Moq;
using Xunit;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.Extension;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Application.Features.Adm.Account.Registration;

namespace IMDb.Server.Application.Tests.Features.Adm.Account;

public class RegistrationAccountAdminCommandHandlerTest
{
    private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
    private readonly Mock<IAdminRepository> adminRepositoryMock = new();
    private readonly Mock<ICryptographyService> cryptoGraphyServiceMock = new();

    [Fact]
    public async Task Handle_WhenEmailAndUserNameAreUnique_ShouldRegisterAdmin()
    {
        //Arrange
        var username = "testName";
        var email = "testEmail@test.com";

        var salt = Array.Empty<byte>();
        var passwordCryptograph = Array.Empty<byte>();

        var adm = new Admin
        {
            Email = email,
            Username = username,
            PasswordHashSalt = salt,
            PasswordHash = passwordCryptograph
        };

        Admin actualAdm = null!;

        var request = new RegistrationAccountAdmCommand("testName", "testEmail@test.com", "testPassword");

        var handler = new RegistrationAccountAdmCommandHandler(unitOfWorkMock.Object, cryptoGraphyServiceMock.Object, adminRepositoryMock.Object);

        adminRepositoryMock.Setup(arm => arm.IsUniqueUsername(username, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        adminRepositoryMock.Setup(arm => arm.IsUniqueEmail(email, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        adminRepositoryMock.Setup(arm => arm.Create(It.IsAny<Admin>(), It.IsAny<CancellationToken>())).Callback((Admin a, CancellationToken ct) => actualAdm = a).Returns(Task.CompletedTask);

        cryptoGraphyServiceMock.Setup(csm => csm.CreateSalt()).Returns(salt);
        cryptoGraphyServiceMock.Setup(csm => csm.Hash(request.Password, salt)).Returns(passwordCryptograph);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        adminRepositoryMock.Verify(arm => arm.Create(actualAdm, CancellationToken.None));
        unitOfWorkMock.VerifyAll();

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(adm, actualAdm, true);
    }

    [Fact]
    public async Task Handle_WhenUsernameIsNotUnique_ShouldFailRegister()
    {
        adminRepositoryMock.Setup(arm => arm.IsUniqueUsername("testNamewr", It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var request = new RegistrationAccountAdmCommand("testNamewr", "", "");

        var handler = new RegistrationAccountAdmCommandHandler(unitOfWorkMock.Object, cryptoGraphyServiceMock.Object, adminRepositoryMock.Object);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenEmailIsNotUnique_ShouldFailRegister()
    {
        adminRepositoryMock.Setup(arm => arm.IsUniqueEmail("testEmailwr@test.com", It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var request = new RegistrationAccountAdmCommand("", "testEmailwr@test.com", "");

        var handler = new RegistrationAccountAdmCommandHandler(unitOfWorkMock.Object, cryptoGraphyServiceMock.Object, adminRepositoryMock.Object);

        var response = await handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
