namespace IMDb.Server.Application.Features.MoviesManagement.GetAllMovies;

public record GetAllMoviesQueryResponse(
    int Id,
    int Rating,
    string Name
);
