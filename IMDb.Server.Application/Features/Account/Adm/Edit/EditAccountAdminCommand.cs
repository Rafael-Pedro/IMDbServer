using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Edit;

public record EditAccountAdminCommand(string Username, string Password, string Email) : IRequest<Result>;
)
