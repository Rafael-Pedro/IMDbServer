using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Adm.GetActiveUsers;

public record GetActiveUsersQuery(int Page, int PageSize, bool IsDescending) : IRequest<Result<IEnumerable<GetActiveUsersQueryResponse>>>;