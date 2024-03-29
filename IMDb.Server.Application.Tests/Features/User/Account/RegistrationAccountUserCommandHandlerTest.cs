﻿using Xunit;
using NSubstitute;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Application.Extension;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Application.Services.Cryptography;
using IMDb.Server.Application.Features.User.Registration;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Application.Tests.Features.User.Account;

public class RegistrationAccountUserCommandHandlerTest
{
    private readonly RegistrationAccountUserCommandHandler sut;
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IUsersRepository usersRepositoryMock = Substitute.For<IUsersRepository>();
    private readonly ICryptographyService cryptoGraphyServiceMock = Substitute.For<ICryptographyService>();

    public RegistrationAccountUserCommandHandlerTest()
    => sut = new(unitOfWorkMock, cryptoGraphyServiceMock, usersRepositoryMock);

    [Fact]
    public async Task Handle_WhenEmailAndUsernameAreUnique_ShouldRegisterUser()
    {
        //Arrange
        var username = "testName";
        var email = "testEmail@test.com";

        var salt = Array.Empty<byte>();
        var passwordCryptograph = Array.Empty<byte>();

        var user = new Users
        {
            Email = email,
            Username = username,
            PasswordHashSalt = salt,
            PasswordHash = passwordCryptograph
        };

        Users actualUser = null!;

        var request = new RegistrationAccountUserCommand("testName", "testEmail@test.com", "testPassword");

        usersRepositoryMock.IsUniqueUsername(username, Arg.Any<CancellationToken>()).Returns(true);
        usersRepositoryMock.IsUniqueEmail(email, Arg.Any<CancellationToken>()).Returns(true);
        usersRepositoryMock.Create(Arg.Do<Users>(u => actualUser = u), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        cryptoGraphyServiceMock.CreateSalt().Returns(salt);
        cryptoGraphyServiceMock.Hash(request.Password, salt).Returns(passwordCryptograph);

        //Act
        var response = await sut.Handle(request, CancellationToken.None);

        //Assert
        unitOfWorkMock.Received();

        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
        Assert.Equivalent(user, actualUser, true);
    }

    [Fact]
    public async Task Handle_WhenUsernameIsNotUnique_ShouldFailRegister()
    {

        usersRepositoryMock.IsUniqueUsername("testNamewr", Arg.Any<CancellationToken>()).Returns(false);

        var request = new RegistrationAccountUserCommand("testNamewr", "", "");

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

    [Fact]
    public async Task Handle_WhenEmailIsNotUnique_ShouldFailRegister()
    {

        usersRepositoryMock.IsUniqueEmail("testNamewr@gmail.com", Arg.Any<CancellationToken>()).Returns(false);

        var request = new RegistrationAccountUserCommand("", "testNamewr@gmail.com", "");

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }
}
