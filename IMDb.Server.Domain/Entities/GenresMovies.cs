using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class GenresMovies : Entity
{
    public Genres Genres { get; set; }
    public int GenresId { get; set; }
    public Movies Movies { get; set; }
    public int MoviesId { get; set; }
}
