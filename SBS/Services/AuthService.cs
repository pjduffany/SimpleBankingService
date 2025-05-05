using SBS.Models;
using SBS.Models.Entities;
using SBS.Utils;

namespace SBS.Services
{
	public class AuthService(SbsDbContext context, IHttpContextAccessor contextAccessor)
	{
		private readonly SbsDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        private readonly IHttpContextAccessor _contextAccessor =
            contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
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

            // set session user id
            _contextAccessor.HttpContext?.Session.SetInt32("UserId", user.UserId);
            
            return new ResponseResult { Success = true };
        }
    }
}

