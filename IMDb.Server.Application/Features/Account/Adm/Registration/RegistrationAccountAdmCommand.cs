using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;

namespace IMDb.Server.Application.Features.Account.Adm.Registration;

public record RegistrationAccountAdmCommand(string Username, string Email, string Password) : IRequest<Result>, IUserId
{
    public int UserId { get; set; }
}
