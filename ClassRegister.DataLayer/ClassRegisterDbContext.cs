using ClassRegister.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace ClassRegister.DataLayer
{
    public interface IClassRegisterDbContext : IDisposable
    {
        DbSet<Coach> Coaches { get; set; }
        DatabaseFacade Database { get; }

        int SaveChanges();
    }

    public class ClassRegisterDbContext : DbContext, IClassRegisterDbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Coach>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ClassRegisterDB;Trusted_Connection=True");
        }
    }
}
