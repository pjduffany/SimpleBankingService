using System;
using Microsoft.EntityFrameworkCore;
using SBS.Models.Entities;
namespace SBS.Models
{
	public class SbsDbContext : DbContext
	{

		public SbsDbContext(DbContextOptions<SbsDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("users");
		}

        public DbSet<User> Users { get; set; }
    }
}

