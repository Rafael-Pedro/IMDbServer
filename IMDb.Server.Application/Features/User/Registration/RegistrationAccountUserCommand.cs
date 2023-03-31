using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.User.Registration;

public record RegistrationAccountUserCommand(string Username, string Email, string Password) : IRequest<Result<RegistrationAccountUserCommandResponse>>;
