using System;
using SBS.Models.Entities;
namespace SBS.Models
{
	public class SbsDbInitializer
	{
		public static void Initialize(SbsDbContext context)
		{
			if (context.Users.Any())
			{
				return; // DB has already been seeded
			}

			var users = new User[]
			{
				new User{}
			};

			//context.Users.AddRange(users);
			//context.SaveChanges();
		}
	}
}

