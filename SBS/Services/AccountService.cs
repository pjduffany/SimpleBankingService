using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using SBS.Models.Entities;
using SBS.Models;
using SBS.Models.Entities;
using SBS.Utils;

namespace SBS.Services;

public class AccountService (SbsDbContext context, IHttpContextAccessor accessor)
{
    private readonly SbsDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IHttpContextAccessor _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    
    public ResponseResult CreateAccount(int userId, AccountType acctType, int amount)
    {
        try
        {
            var acctNumber = AcctNumGenerator.GenerateAccountNumber(acctType);

            if (_context.Accounts.Any(x => x.AccountNumber == acctNumber)) // SELECT 1 WHERE
            {
                return new ResponseResult
                {
                    Success = false,
                    ErrorMessage = "Account number already exists. Please try again."
                };
            }
            
            _context.Accounts.Add(new Account
            {
                AccountHolder = userId,
                AccountNumber = acctNumber,
                AccountType = acctType,
                Balance = amount,
                CreatedOn = DateTime.UtcNow,
                LastAccessedOn = DateTime.UtcNow,
                Enabled = true
            });
            
            _context.SaveChanges();
            
            return new ResponseResult
            {
                Success = true
            };

        }
        catch (DbUpdateException ex)
        {
            return new ResponseResult
            {
                Success = false,
                ErrorMessage = $"An unexpected exception occurred attempting to create {acctType.ToString()} account. Error: {ex.Message}"
            };
        }
    }
}