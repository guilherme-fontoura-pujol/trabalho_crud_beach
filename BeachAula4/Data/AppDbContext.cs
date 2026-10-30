using BeachAula4.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BeachAula4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Quadra> Quadras => Set<Quadra>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=beachtennis.db");
            }
        }
    }
}