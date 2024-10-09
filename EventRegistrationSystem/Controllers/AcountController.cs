using EventRegistrationSystem.Data;
using EventRegistrationSystem.Models.DTO;
using EventRegistrationSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _contextt;

        public AccountController(IAccount context)
        {
            _contextt = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO data)
        {
            if (ModelState.IsValid)
            {
                var user = await _contextt.Register(data, ModelState);

                if (user != null)
                {
                    // After registration, redirect to login or dashboard
                    return RedirectToAction("Index", "Home");
                }
            }

            // If model state is invalid or registration fails, return to the view
            return View(data);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var user = await _contextt.LoginUser(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                // Add error message for invalid login
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginDto);
            }

            // On successful login, redirect to the home or dashboard
            return RedirectToAction("Index");
        }

       
    }
}
