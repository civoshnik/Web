using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usluga> Uslugi { get; set; }
    }
}