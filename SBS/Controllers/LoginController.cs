using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SBS.Models.Entities;
using SBS.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SBS.Controllers
{
    public class LoginController(AuthService authService) : Controller
    {
        private readonly AuthService _authService = authService ?? throw new ArgumentNullException(nameof(authService));

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(string email, string password)
        {
            var response = _authService.ValidateUser(email, password);

            if (response.Success)
            {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.Error = response.ErrorMessage ?? "Invalid username or password.";
            return View("Index");
        }
    }
}

