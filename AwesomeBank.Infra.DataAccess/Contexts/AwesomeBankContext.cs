using AwesomeBank.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeBank.Infra.DataAccess.Contexts
{
    public class AwesomeBankContext : DbContext
    {
        public DbSet<AccountHolder> AccountHolders { get; set; }

        public AwesomeBankContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=tcp:awesomebank-db-server.database.windows.net,1433;Initial Catalog=AwesomeBank-Db;Persist Security Info=False;User ID=samuel;Password=@dsInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
