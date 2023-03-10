using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Account.User.Registration;

public record RegistrationAccountUserCommand(string Username, string Password, string Email) : IRequest<Result>;
