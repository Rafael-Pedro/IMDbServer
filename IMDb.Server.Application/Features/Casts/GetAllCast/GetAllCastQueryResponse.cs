namespace IMDb.Server.Application.Features.Casts.GetAllCast;

public record GetAllCastQueryResponse(
    int Id,
    string Name,
    string Description,
    DateTime BitrhDate
);