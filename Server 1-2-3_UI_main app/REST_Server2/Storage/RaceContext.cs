using System.Data.Entity;

namespace Storage
{
    public class RaceContext : DbContext
    {
        public RaceContext() : base("TrackRecord")
        {
        }

        public DbSet<Race> Race { get; set; }
    }
}
