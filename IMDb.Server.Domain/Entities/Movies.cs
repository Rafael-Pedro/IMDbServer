using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class Movies : Entity
{
    public int Votes { get; set; }
    public decimal Rating { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<Cast> Cast { get; set; }
    public IEnumerable<Genres> Genders { get; set; }
    public IEnumerable<Vote> MyProperty { get; set; }

}
