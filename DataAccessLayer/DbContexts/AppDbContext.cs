using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Param> Params => Set<Param>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Section>()
                .HasMany(s => s.Params)
                .WithOne(p => p.Section)
                .HasForeignKey(p => p.SectionId);

            modelBuilder.Entity<Param>()
                .HasIndex(p => new { p.SectionId, p.Name })
                .IsUnique();
        }
    }
}
