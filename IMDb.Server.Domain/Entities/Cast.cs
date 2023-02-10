using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities
{
    public class Cast : User
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<Movies> DirectedMovies { get; set; }
        public IEnumerable<Movies> ActedMovies { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
