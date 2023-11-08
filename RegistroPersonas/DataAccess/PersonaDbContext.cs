using RegistroPersonas.Models;
using RegistroPersonas.Utilities;
using Microsoft.EntityFrameworkCore;

namespace RegistroPersonas.DataAccess
{
    public class PersonaDbContext : DbContext
    {
        public DbSet<Personas> Personas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={ConnectionDB.ReturnPathDb("Personas.db")}";
            optionsBuilder.UseSqlite(connectionDb);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasKey(col => col.ID);
                entity.Property(col => col.ID).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
