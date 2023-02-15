using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class CastActMovies : Entity
{
    public int CastActId { get; set; }
    public Cast CastAct { get; set; } = default!;
    public int MoviesActId { get; set; }
    public Movies MoviesAct { get; set; } = default!;
}
