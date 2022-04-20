using Microsoft.EntityFrameworkCore;

namespace zomato.Data
{
    public class ZomatoContext : DbContext
    {
        public ZomatoContext()
        {

        }
        public ZomatoContext(DbContextOptions<ZomatoContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
