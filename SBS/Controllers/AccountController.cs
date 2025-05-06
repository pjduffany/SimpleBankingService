using Microsoft.AspNetCore.Mvc;
using SBS.Models.Entities;
using SBS.Services;

namespace SBS.Controllers;

public class AccountController(AccountService acctService) : Controller
{
    private readonly AccountService _acctService = acctService ?? throw new ArgumentNullException(nameof(acctService));

    
    [HttpPost]
    public IActionResult Logout()
    {
        // Remove specific session key(s)
        HttpContext.Session.Remove("UserId");

        // OR clear all session values
        HttpContext.Session.Clear();

        // Redirect to login or home page
        return RedirectToAction("Index", "Login");
    }
    // GET: /<controller>/
    [HttpGet]
    public IActionResult Index()
    {
        var user =  _acctService.GetUserInfo();
        ViewBag.WelcomeMessage = $"Welcome {user.FirstName} {user.LastName}!";
        return View();
    }


    [HttpGet]
    public IActionResult CreateAccount()
    {
        return View("CreateAccount");
    }
    
    [HttpPost]
    public IActionResult CreateAccount(AccountType accountType, int depositAmount)
    {
        var responseResult = _acctService.CreateAccount(accountType, depositAmount);

        if (responseResult.Success)
        {
            ViewBag.Message = "Account created successfully";
            return View();
        }

        ViewBag.ErrorMessage = responseResult.ErrorMessage;
        return View(); // Re-show form with error
    }

    [HttpGet]
    public async Task<IActionResult> AccountOverview()
    {
        var user =  _acctService.GetUserInfo();
        var accounts = await _acctService.GetAccountInfo(user);
        return View("AccountOverview", accounts);
    }
    
}    