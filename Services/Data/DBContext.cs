using System;
using Bank.Model;
using Microsoft.EntityFrameworkCore;
namespace Bank.Console.Data
{
    public class DBContext: DbContext
    {
        private const string connectionString = "Server=localhost,1401;Database=BankingApp;User= sa;Password=omsairam@1234;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<Model.Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
