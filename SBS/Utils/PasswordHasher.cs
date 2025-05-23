﻿using System.Security.Cryptography;
using System.Text;

namespace SBS.Utils
{
	public static class PasswordHasher
	{
		public static bool Verify(string password, byte[] hash, byte[] salt)
		{
			using var hmac = new HMACSHA256(salt);

			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

			return hash.SequenceEqual(computedHash);
		}

		public static (byte[] Hash, byte[] Salt) HashPassword(string password)
		{
			using var hmac = new HMACSHA256();
			var salt = hmac.Key;
			var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			
			return (hash, salt);
		}
	}
}

