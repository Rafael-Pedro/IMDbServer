using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class CastMovies : Entity
{
    public int CastId { get; set; }
    public Cast Cast { get; set; } = default!;
    public int MoviesId { get; set; }
    public Movies Movies { get; set; } = default!;
}
