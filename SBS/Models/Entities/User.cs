using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBS.Models.Entities
{
	public class User
    {
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Column("email_address")]
        public string Email { get; set; } //= string.Empty;
        [Required]
        [Column("street")]
        public string Street { get; set; } = string.Empty;
        [Required]
        [Column("city")]
        public string City { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "State code must be two uppercase letters.")]
        [Column("state")]
        public string StateCode { get; set; } = string.Empty;
        [Required]
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [Required]
        [Column("zip_code")]
        public string ZipCode { get; set; } = string.Empty;
        [Required]
        [Column("salt")]
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        [Required]
        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Column("enabled")]
        public bool Enabled { get; set; }
	}
}

