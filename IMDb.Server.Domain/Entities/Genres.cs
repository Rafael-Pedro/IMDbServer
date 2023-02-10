using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class Genres : Entity
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<GenresMovies> GenresMovies { get; set; } = Array.Empty<GenresMovies>();
}
