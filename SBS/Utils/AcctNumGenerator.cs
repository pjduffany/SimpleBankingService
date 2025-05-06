using System.Diagnostics;
using SBS.Models.Entities;

namespace SBS.Utils;

public static class AcctNumGenerator
{
    public static string GenerateAccountNumber(AccountType accountType)
    {
        var rng = new Random();
        return accountType switch
        {
            AccountType.Checking =>  1 + rng.Next(100000000, 999999999).ToString(),
            AccountType.Savings =>   2 + rng.Next(100000000, 999999999).ToString(),
            AccountType.Investing => 3 + rng.Next(100000000, 999999999).ToString(),
            _ => throw new ArgumentException("Invalid account type provided.")
        };
    }
}