using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Account.Admin.Registration;

public record RegistrationAccountCommand(string Login, string Password) : IRequest<Result>
{

}
