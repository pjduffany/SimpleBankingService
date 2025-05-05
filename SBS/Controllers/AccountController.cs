using Microsoft.AspNetCore.Mvc;
using SBS.Models.Entities;
using SBS.Services;

namespace SBS.Controllers;

public class AccountController(AccountService acctService) : Controller
{
    private readonly AccountService _acctService = acctService ?? throw new ArgumentNullException(nameof(acctService));

    // GET: /<controller>/
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult CreateAccount(AccountType accountType, int amount)
    {
        int userId = HttpContext.Session.GetInt32("UserId") != null ? (int)HttpContext.Session.GetInt32("UserId") : -1;
        if (userId == -1)
        {
            ViewBag.Error = "Unable to retrieve user id from Session";
            return View();
        }
        
        var responseResult = _acctService.CreateAccount(userId, accountType, amount);

        if (responseResult.Success)
        {
            
        }
        return View();
    }

    [HttpGet]
    public IActionResult AccountOverview()
    {
        return View();
    }
}    