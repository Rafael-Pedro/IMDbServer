using FluentResults;
using IMDb.Server.Application.UserInfo;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Disable;

public record DisableAccountAdmCommand() : IRequest<Result>, IUserId
{
    public int UserId { get; set; }
}
