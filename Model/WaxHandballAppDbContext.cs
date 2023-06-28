using Angular_Test_App.Model.Players;
using Angular_Test_App.Model.Shop;
using Microsoft.EntityFrameworkCore;

namespace Angular_Test_App.Model
{
    public class WaxHandballAppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }

        public WaxHandballAppDbContext(DbContextOptions<WaxHandballAppDbContext> options) : base(options)
        {

        }
    }
}
