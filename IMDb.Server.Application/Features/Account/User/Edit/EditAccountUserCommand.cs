using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Account.User.Edit;

public record EditAccountUserCommand(string Username, string Email, string? Password) : IRequest<Result>;
