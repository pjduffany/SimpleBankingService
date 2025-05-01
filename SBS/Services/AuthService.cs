using System;
using SBS.Models;
using SBS.Models.Entities;
using SBS.Utils;

namespace SBS.Services
{
	public class AuthService
	{
		private readonly SbsDbContext _context;

		public AuthService(SbsDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

        public ResponseResult ValidateUser(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            Console.WriteLine("Email address:" + email);
            if (user == null)
            {
                return new ResponseResult
                {
                    Success = false,
                    ErrorMessage = "User not found."
                };
            }
            Console.WriteLine($"Email: {email} | Password: {password} | Hash: {user.PasswordHash} | Salt: {user.Salt}");
            var passwordValid = PasswordHasher.Verify(password, user.PasswordHash, user.Salt);

            if (!passwordValid)
            {
                return new ResponseResult
                {
                    Success = false,
                    ErrorMessage = "Invalid password."
                };
            }

            return new ResponseResult { Success = true };
        }
    }
}

