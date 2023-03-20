using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.Account.GetActiveUsers;

public record GetActiveUsersCommand() : IRequest<Result>;