using FluentResults;
namespace IMDb.Server.Application.Extension;

public class ApplicationNotFoundError : Error
{
    public ApplicationNotFoundError(string error) : base(error)
    {
    }
}
