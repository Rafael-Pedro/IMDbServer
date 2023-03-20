using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.Account.Disable;

public record DisableAccountAdmCommand() : IRequest<Result>;