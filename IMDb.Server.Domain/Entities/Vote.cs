using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities;

public class Vote : Entity
{
    public int UserId { get; set; }
    public Users User { get; set; }
    public int MovieId { get; set; }
    public Movies Movie { get; set; }
}
