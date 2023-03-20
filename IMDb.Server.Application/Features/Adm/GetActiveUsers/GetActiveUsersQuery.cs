using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.GetActiveUsers;

public record GetActiveUsersQuery() : IRequest<Result>;