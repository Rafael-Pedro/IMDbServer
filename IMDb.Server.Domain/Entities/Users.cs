using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities
{
    public class Users : Account
    {
        public IEnumerable<Vote> Votes { get; set; } = default!;
    }
}
