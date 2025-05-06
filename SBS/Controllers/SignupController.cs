using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SBS.Models.Entities;
using SBS.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SBS.Controllers
{
    public class SignupController(SignupService signupService) : Controller
    {
        private readonly SignupService _signupService = signupService ?? throw new ArgumentNullException(nameof(signupService));

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(SignupRequest request)
        {
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{10,}$";
            bool isValid = Regex.IsMatch(request.Password, pattern);

            if (!isValid)
            {
                ViewBag.Error = "Password must be at least 10 characters long and include an uppercase letter, lowercase letter, number, and special character.";
                return View("Index");
            }
            
            var result = _signupService.RegisterUser(request);

            if (result.Success)
            {
                return RedirectToAction("Index", "Login"); // redirect user to login after registration
            }
            ViewBag.Error = result.ErrorMessage ?? "Registration failed.";
            Console.WriteLine("Registration failed: " + result.ErrorMessage);
            return View("Index");
        }
    }
}

