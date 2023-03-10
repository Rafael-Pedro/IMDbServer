using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Account.User.Edit;

public record EditAccountUserCommand(string Username, string Password, string Email) : IRequest<Result>;
