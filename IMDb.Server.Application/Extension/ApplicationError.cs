using FluentResults;
namespace IMDb.Server.Application.Extension;

public class ApplicationError : Error
{
    public ApplicationError(string error) : base(error)
    {
    }
}