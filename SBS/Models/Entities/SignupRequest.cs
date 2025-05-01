using System.ComponentModel.DataAnnotations;

namespace SBS.Models.Entities
{
	public class SignupRequest
	{
        [Required]
        public string EmailAddress { get; set; } //= string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "State code must be two uppercase letters.")]
        public string State { get; set; } = string.Empty;
        [Required]
        public string ZipCode { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}

