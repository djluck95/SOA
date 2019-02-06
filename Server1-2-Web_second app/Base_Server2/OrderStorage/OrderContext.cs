using System.Data.Entity;

namespace OrderStorage
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("DTM_DB")
        {
        }

        public DbSet<Order> Order { get; set; }

        public DbSet<Stock> Stock { get; set; }
    }
}
