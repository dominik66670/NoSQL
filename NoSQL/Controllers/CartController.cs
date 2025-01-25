using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NoSQL.Data;
using NoSQL.Models;
using System.Text.Json;

namespace NoSQL.Controllers
{
    public class CartController : Controller
    {
        private readonly NoSQLContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDistributedCache _distributedCache;
        public CartController(NoSQLContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IDistributedCache distributedCache) 
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _distributedCache = distributedCache;

        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var sessionKey = $"Session_{user.Id}";
            var sessionData = await _distributedCache.GetStringAsync(sessionKey);
            if (sessionData != null) 
            {
                Cart cart = JsonSerializer.Deserialize<UserSession>(sessionData).personalCart;
                return View(cart);
            }
            
            return View();
        }
        public async Task<IActionResult> AddToCart(int Id) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) 
            {
                return RedirectToAction("Index","Books");
            }
            Book book = await _context.Book.FirstOrDefaultAsync(b => b.id == Id);
            if (book == null)
            {
                return RedirectToAction("Index", "Books");
            }
            var sessionKey = $"Session_{user.Id}";
            var sessionData = await _distributedCache.GetStringAsync(sessionKey);
            if (sessionData != null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };
                UserSession UserSession = JsonSerializer.Deserialize<UserSession>(sessionData);
                UserSession.personalCart.Add(book);
                var newSessionData = JsonSerializer.Serialize(UserSession);
                await _distributedCache.SetStringAsync(sessionKey, newSessionData,options);
                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("Index", "Books");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int Id) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Book book = await _context.Book.FirstOrDefaultAsync(b => b.id == Id);
            if (book == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            var sessionKey = $"Session_{user.Id}";
            var sessionData = await _distributedCache.GetStringAsync(sessionKey);
            if (sessionData != null)
            {

                UserSession UserSession = JsonSerializer.Deserialize<UserSession>(sessionData);
                if (UserSession.personalCart.Any(book => book.id == Id)) 
                {
                    var options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    };
                    UserSession.personalCart.Remove(UserSession.personalCart.Find(book => book.id==Id));
                    var newSessionData = JsonSerializer.Serialize(UserSession);
                    await _distributedCache.SetStringAsync(sessionKey, newSessionData,options);
                }
                
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
