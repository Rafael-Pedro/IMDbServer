using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;

namespace IMDb.Server.Application.Features.Adm.Account.Registration;

public record RegistrationAccountAdmCommand(string Username, string Email, string Password) : IRequest<Result<RegistrationAccountAdmCommandResponse>>;
