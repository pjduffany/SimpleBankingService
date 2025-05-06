using System;
using Microsoft.EntityFrameworkCore;
using SBS.Models.Entities;
namespace SBS.Models
{
	public class SbsDbContext(DbContextOptions<SbsDbContext> options) : DbContext(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("users");
			modelBuilder.Entity<Account>().ToTable("accounts");
			modelBuilder.Entity<Account>()
				.Property(a => a.AccountType)
				.HasConversion<string>();
		}

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
    }
}

