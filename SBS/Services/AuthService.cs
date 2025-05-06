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
            var passwordValid = PasswordHasher.Verify(password, user.PasswordHash, user.Salt);

            if (!passwordValid)
            {
                return new ResponseResult
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password."
                };
            }
            SetUserId(user);
            
            return new ResponseResult { Success = true };
        }

        private void SetUserId(User user)
        {
            // set session user id
            _contextAccessor.HttpContext?.Session.SetInt32("UserId", user.UserId);
        }
    }
}

