using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SBS.Models.Entities;
using SBS.Models;
using SBS.Models.Entities;
using SBS.Utils;

namespace SBS.Services;

public class AccountService (SbsDbContext context, IHttpContextAccessor accessor)
{
    private readonly SbsDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IHttpContextAccessor _contextAccessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    
    public ResponseResult CreateAccount(AccountType acctType, int amount)
    {
        try
        {
            var acctNumber = AcctNumGenerator.GenerateAccountNumber(acctType);
            var userId = GetUserIdFromSession();
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

    public async Task<List<Account>> GetAccountInfo(User user)
    {
        var userId = GetUserIdFromSession();
        return await _context.Accounts.
            Where(x => x.AccountHolder == userId).
            ToListAsync();
    }

    public User GetUserInfo()
    {
        var userId = GetUserIdFromSession();
        
        return _context.Users.SingleOrDefault(x => x.UserId == userId) ?? throw new InvalidOperationException($"User not found for user: {userId}.");

    }

    private int GetUserIdFromSession()
    {
        var userId = _contextAccessor.HttpContext?.Session.GetInt32("UserId") != null 
            ? Convert.ToInt32(_contextAccessor.HttpContext.Session.GetInt32("UserId")) 
            : -1;

        if (userId == -1)
        {
            throw new InvalidOperationException("UserId was not found for the current session.");
        } 
        return userId;
    }
    
    
}