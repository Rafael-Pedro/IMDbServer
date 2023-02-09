using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities
{
    public class Users : User
    {
        public IEnumerable<Vote> Votes { get; set; }
    }
}
