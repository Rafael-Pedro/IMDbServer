using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class CastDirectMovies:Entity
{
    public int CastDirectorId { get; set; }
    public Cast CastDirector { get; set; } = default!;
    public int MoviesDirectId { get; set; }
    public Movies MoviesDirect { get; set; } = default!;
}
