using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBS.Models.Entities
{
	public class Account
	{
		[Required]
		[Column("account_id")]
		public int AccountId { get; set; }
		[Required]
		[Column("account_number")]
		public string AccountNumber { get; set; }
		[Required]
		[Column("account_type")]
		public AccountType AccountType { get; set; }
		[Required]
		[Column("balance")]
		public int Balance { get; set; }
		[Required]
		[Column("user_id")]
		public int AccountHolder { get; set; }
		[Required]
		[Column("created_on")]
		public DateTime CreatedOn { get; set; }
		[Required]
		[Column("last_accessed_on")]
		public DateTime LastAccessedOn { get; set; }
		[Required]
		[Column("enabled")]
		public Boolean Enabled { get; set; }
	}
}

