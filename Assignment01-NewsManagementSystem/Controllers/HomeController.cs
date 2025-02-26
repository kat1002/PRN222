using Assignment01_NewsManagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Assignment01_NewsManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var newsArticles = await WebManager.Instance().Context.NewsArticles
                .Where(n => n.NewsStatus.Equals(true))
                .Include(n => n.Category)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            return View(newsArticles);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Console.WriteLine(email + " " + password);

            var user = WebManager.Instance().SystemAccountDAO.GetAccountByCredentials(email, password);

            if (user == null)
            {
                ViewData["Error"] = "Invalid email or password.";
                return View("Login");  // Stay on login page instead of Index
            }

            string accountRole = "";

            switch (user.AccountRole)
            {
                case 0:
                    accountRole = "Admin";
                    break;

                case 1:
                    accountRole = "Lecturer";
                    break;

                case 2:
                    accountRole = "Staff";
                    break;

                default:
                    break;
            }

            Console.WriteLine(user.ToString());

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()), // Unique User ID
                new Claim(ClaimTypes.Name, user.AccountName), // Display Name
                new Claim(ClaimTypes.Email, user.AccountEmail), // Email
                new Claim(ClaimTypes.Role, accountRole) // Role (Admin, User, etc.)
            };


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/Article/5
        public async Task<IActionResult> Article(string id)
        {
            var newsArticle = WebManager.Instance().NewsArticleDAO.GetArticleById(id);

            Console.WriteLine(newsArticle.ToString());

            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }
    }
}
