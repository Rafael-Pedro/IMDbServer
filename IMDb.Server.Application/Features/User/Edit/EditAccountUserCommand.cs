using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.User.Edit;

public record EditAccountUserCommand(string Username, string Email, string? Password) : IRequest<Result>;
