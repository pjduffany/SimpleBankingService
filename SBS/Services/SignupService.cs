using System;
using SBS.Models.Entities;
using SBS.Models;
using SBS.Utils;
using Microsoft.EntityFrameworkCore;

namespace SBS.Services
{
	public class SignupService(SbsDbContext context)
	{
		private readonly SbsDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

		public ResponseResult RegisterUser(SignupRequest request)
		{
			Console.WriteLine($"EMAIL FROM REQUEST: {request.EmailAddress}");

			if (_context.Users.Any(user => user.Email == request.EmailAddress))
			{
				return new ResponseResult
				{
					Success = false,
					ErrorMessage = "A user with this email already exists."
				};
			}
			else
			{
				var (hash, salt) = PasswordHasher.HashPassword(request.Password);
				try
				{
					_context.Users.Add(new User
					{
						FirstName = request.FirstName,
						LastName = request.LastName,
						Email = request.EmailAddress,
						PasswordHash = hash,
						Salt = salt,
						Street = request.Street,
						City = request.City,
						StateCode = request.State,
							ZipCode = request.ZipCode,
						CreatedOn = DateTime.UtcNow,
							Enabled = true
					});

					_context.SaveChanges();
					return new ResponseResult { Success = true };
				}
				catch (DbUpdateException ex)
				{
					return new ResponseResult
					{
						Success = false,
						ErrorMessage = $"Database error: {ex.InnerException?.Message ?? ex.Message}"
					};
				}
				catch (Exception ex)
				{
					return new ResponseResult
					{
						Success = false,
						ErrorMessage = $"Unexpected error: {ex.Message}"
					};
				}
			}
		}
	}
}

