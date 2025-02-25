using Assignment01_NewsManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment01_NewsManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class NewsArticlesController : Controller
    {

        public NewsArticlesController(FunewsManagementContext context)
        {

        }

        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID
            var userRole = User.FindFirstValue(ClaimTypes.Role); // Get logged-in user role

            var funewsManagementContext = WebManager.Instance().Context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy);

            short accountId = short.Parse(userId);

            // If user is a Lecturer, filter by CreatedById
            if (userRole == "Lecturer")
            {
                return View(funewsManagementContext.Where(n => n.NewsStatus.HasValue.Equals(true)));
            }

            return View(await funewsManagementContext.ToListAsync());
        }


        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = WebManager.Instance().Context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefault(n => n.NewsArticleId == id);


            var modifiedby = WebManager.Instance().Context.SystemAccounts.FirstOrDefault(a => a.AccountId.Equals(newsArticle.UpdatedById));

            if (newsArticle == null)
            {
                return NotFound();
            }

            // Pass names to ViewBag
            ViewBag.CategoryName = newsArticle.Category?.CategoryName ?? "Unknown";
            ViewBag.CreatedByName = newsArticle.CreatedBy?.AccountName ?? "Unknown";
            ViewBag.UpdatedByName = modifiedby.AccountName;

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsArticleId, NewsTitle, Headline, NewsContent, NewsSource, CategoryId, NewsStatus")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                // Get the current user ID
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Set created and modified metadata
                newsArticle.CreatedById = short.Parse(currentUserId);
                newsArticle.UpdatedById = short.Parse(currentUserId);
                newsArticle.CreatedDate = DateTime.Now;
                newsArticle.ModifiedDate = DateTime.Now;

                // Save to the database
                WebManager.Instance().Context.NewsArticles.Add(newsArticle);
                await WebManager.Instance().Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await WebManager.Instance().Context.NewsArticles.FindAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            var createdByUser = WebManager.Instance().Context.SystemAccounts.FirstOrDefault(u => u.AccountId == newsArticle.CreatedById);
            var updatedByUser = WebManager.Instance().Context.SystemAccounts.FirstOrDefault(u => u.AccountId == newsArticle.UpdatedById);

            ViewBag.CreatedByName = createdByUser != null ? createdByUser.AccountName : "Unknown";
            ViewBag.UpdatedByName = updatedByUser != null ? updatedByUser.AccountName : "Unknown";


            ViewBag.CategoryId = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(WebManager.Instance().Context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NewsArticleId,NewsTitle,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        {
            if (id != newsArticle.NewsArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the current user ID
                    string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    newsArticle.UpdatedById = short.Parse(currentUserId);
                    newsArticle.ModifiedDate = newsArticle.ModifiedDate = DateTime.Now;

                    WebManager.Instance().Context.Update(newsArticle);
                    await WebManager.Instance().Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsArticleExists(newsArticle.NewsArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(WebManager.Instance().Context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await WebManager.Instance().Context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            var modifiedby = WebManager.Instance().Context.SystemAccounts.FirstOrDefault(a => a.AccountId.Equals(newsArticle.UpdatedById));

            // Pass names to ViewBag
            ViewBag.UpdatedByName = modifiedby.AccountName;

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var newsArticle = await WebManager.Instance().Context.NewsArticles.FindAsync(id);
            if (newsArticle != null)
            {
                WebManager.Instance().Context.NewsArticles.Remove(newsArticle);
            }

            await WebManager.Instance().Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(string id)
        {
            return WebManager.Instance().Context.NewsArticles.Any(e => e.NewsArticleId == id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Report(DateTime? StartDate, DateTime? EndDate)
        {
            var newsQuery = WebManager.Instance().Context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .AsQueryable();

            // Apply date filters if selected
            if (StartDate.HasValue)
            {
                newsQuery = newsQuery.Where(n => n.CreatedDate >= StartDate.Value);
            }
            if (EndDate.HasValue)
            {
                newsQuery = newsQuery.Where(n => n.CreatedDate <= EndDate.Value);
            }

            // Sort by CreatedDate in descending order
            newsQuery = newsQuery.OrderByDescending(n => n.CreatedDate);

            return View(await newsQuery.ToListAsync());
        }
    }
}
