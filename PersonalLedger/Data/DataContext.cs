using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLedger.Models;

namespace PersonalLedger.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Ignore(x => x.Balance);
            modelBuilder.Entity<Account>().Property(x => x.DefaultAmt).HasColumnType("money");
            modelBuilder.Entity<Account>().Property(x => x.Institution).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Account>().Property(x => x.Interest).HasDefaultValue(0);
            modelBuilder.Entity<Account>().Property(x => x.Limit).HasColumnType("money");
            modelBuilder.Entity<Account>().Property(x => x.Name).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Account>().Property(x => x.Number).HasColumnType("nvarchar(128)");

            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Category>().Property(x => x.Type).HasColumnType("nvarchar(32)");

            modelBuilder.Entity<Transaction>().Property(x => x.Amount).HasColumnType("money");
        }
    }  //context
}  //namespace
