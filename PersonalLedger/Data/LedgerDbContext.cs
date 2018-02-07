using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLedger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace PersonalLedger.Data
{
    public class LedgerDbContext : IdentityDbContext<IdentityUser>
    {
        public LedgerDbContext(DbContextOptions<LedgerDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure the Identity model
            base.OnModelCreating(modelBuilder);
            //configure the application model
            modelBuilder.Entity<Account>().Ignore(x => x.Balance);
            modelBuilder.Entity<Account>().Property(x => x.DefaultAmt).HasColumnType("money");
            modelBuilder.Entity<Account>().Property(x => x.Institution).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Account>().Property(x => x.Interest).HasDefaultValue(0);
            modelBuilder.Entity<Account>().Property(x => x.Limit).HasColumnType("money");
            modelBuilder.Entity<Account>().Property(x => x.Name).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Account>().Property(x => x.Number).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Account>().Property(x => x.User).HasColumnType("nvarchar(128)");

            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnType("nvarchar(128)");
            modelBuilder.Entity<Category>().Property(x => x.Type).HasColumnType("nvarchar(32)");
            modelBuilder.Entity<Category>().Property(x => x.User).HasColumnType("nvarchar(128)");

            modelBuilder.Entity<Transaction>().Property(x => x.Amount).HasColumnType("money");
            modelBuilder.Entity<Transaction>().Property(x => x.User).HasColumnType("nvarchar(128)");
        }
    }  //context
}  //namespace
