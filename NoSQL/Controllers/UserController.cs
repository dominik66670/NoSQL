using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoSQL.Data;
using NoSQL.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace NoSQL.Controllers
{
    public class UserController : Controller
    {
        private readonly NoSQLContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDistributedCache _distributedCache;

        public UserController(NoSQLContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IDistributedCache distributedCache)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _distributedCache = distributedCache;

        }

        public IActionResult Index()
        {
            var users =_context.Users.ToList(); // Pobiera wszystkich użytkowników
            return View(users);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Nieprawidłowa nazwa użytkownika lub hasło.");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Zapisz sesję w Redis
                UserSession userSession = new UserSession() { UserName= user.UserName,  Email=user.Email, personalCart = new Cart()};
                var sessionData = JsonSerializer.Serialize(userSession);
                await _distributedCache.SetStringAsync($"Session_{user.Id}", sessionData);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Nieprawidłowa nazwa użytkownika lub hasło.");
            return View();
        }

        // Wylogowanie użytkownika
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var sessionKey = $"Session_{user.Id}";
            await _distributedCache.RemoveAsync(sessionKey);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    // Automatyczne logowanie po rejestracji
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }
    }

}


