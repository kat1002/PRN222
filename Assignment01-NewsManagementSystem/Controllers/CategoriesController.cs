using Assignment01_NewsManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment01_NewsManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class CategoriesController : Controller
    {
        private FunewsManagementContext _context;
        public CategoriesController(FunewsManagementContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var funewsManagementContext = WebManager.Instance().Context.Categories.Include(c => c.ParentCategory);
            return View(await funewsManagementContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await WebManager.Instance().Context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            ViewBag.ParentCategoryId = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,CategoryDesciption,ParentCategoryId,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                // Get the max existing CategoryId and increment it manually
                short maxId = (short)(WebManager.Instance().Context.Categories.Any() ? WebManager.Instance().Context.Categories.Max(c => c.CategoryId) : 0);
                category.CategoryId = (short)(maxId + 1);

                WebManager.Instance().Context.Add(category);
                await WebManager.Instance().Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentCategoryId"] = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }


        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await WebManager.Instance().Context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            ViewData["ParentCategories"] = new SelectList(
                WebManager.Instance().Context.Categories, // Prevent self-reference
                "CategoryId",
                "CategoryName",
                category.ParentCategoryId // Preselect current parent category
            );

            return View(category);
        }


        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("CategoryId,CategoryName,CategoryDesciption,ParentCategoryId,IsActive")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Detach existing category entity to avoid tracking conflicts
                    var existingCategory = WebManager.Instance().Context.Categories
                        .FirstOrDefault(c => c.CategoryId == category.CategoryId);

                    if (existingCategory != null)
                    {
                        WebManager.Instance().Context.Entry(existingCategory).State = EntityState.Detached;
                    }

                    WebManager.Instance().Context.Update(category);
                    await WebManager.Instance().Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            ViewData["ParentCategoryId"] = new SelectList(WebManager.Instance().Context.Categories, "CategoryId", "CategoryId", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await WebManager.Instance().Context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var category = await WebManager.Instance().Context.Categories.FindAsync(id);
            if (category != null)
            {
                WebManager.Instance().Context.Categories.Remove(category);
            }

            await WebManager.Instance().Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(short id)
        {
            return WebManager.Instance().Context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
