using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace zomato.Data
{
    public class ZomatoContext : DbContext
    {
        public ZomatoContext()
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
