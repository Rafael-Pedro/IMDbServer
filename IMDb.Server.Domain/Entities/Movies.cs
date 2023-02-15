using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class Movies : Entity
{
    public int TotalVotes { get; set; } = 0;
    public decimal Rating { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public IEnumerable<CastActMovies> ActorCast { get; set; } = default!;
    public IEnumerable<CastDirectMovies> DirectorCast { get; set; } = default!;
    public IEnumerable<Vote> Votes { get; set; } = default!;
    public IEnumerable<GenresMovies> GenresMovies { get; set; } = default!;
}
