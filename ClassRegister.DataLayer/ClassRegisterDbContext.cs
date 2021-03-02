using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace ClassRegister.DataLayer
{
    public interface IClassRegisterDbContext : IDisposable
    {
        DatabaseFacade Database { get; }
        int SaveChanges();
    }

    public class ClassRegisterDbContext : DbContext, IClassRegisterDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ClassRegisterDB;Trusted_Connection=True");
        }
    }
}
