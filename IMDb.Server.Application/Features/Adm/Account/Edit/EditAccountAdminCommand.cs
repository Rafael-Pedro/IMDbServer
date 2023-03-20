using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.Account.Edit;

public record EditAccountAdminCommand(string Username, string Email, string? Password) : IRequest<Result>;
