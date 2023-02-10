using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class GenresMovies : Entity
{
    public Genres Genres { get; set; } = default!;
    public int GenresId { get; set; }
    public Movies Movies { get; set; } = default!;
    public int MoviesId { get; set; }
}
