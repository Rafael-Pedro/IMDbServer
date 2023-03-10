using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Disable;

public record DisableAccountAdmCommand() : IRequest<Result>;