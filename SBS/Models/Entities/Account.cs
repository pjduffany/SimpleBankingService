using System;
namespace SBS.Model.Entities
{
	public class Account
	{
		public int AccountId { get; set; }
		public AccountType AccountType { get; set; }
		public int Balance { get; set; }
		public string AccountHolder { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime LastAccessedOn { get; set; }
		public Boolean Enabled { get; set; }
	}
}

