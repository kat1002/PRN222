using Assignment01_NewsManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment01_NewsManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemAccountsController : Controller
    {

        public SystemAccountsController(FunewsManagementContext context)
        {

        }

        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            return View(await WebManager.Instance().Context.SystemAccounts.ToListAsync());
        }

        // GET: SystemAccounts/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await WebManager.Instance().Context.SystemAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        // GET: SystemAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        {
            if (ModelState.IsValid)
            {
                // Get the max existing AccountId and increment it manually
                short maxId = (short)(WebManager.Instance().Context.SystemAccounts.Any() ? WebManager.Instance().Context.SystemAccounts.Max(a => a.AccountId) : 0);
                systemAccount.AccountId = (short)(maxId + 1);

                WebManager.Instance().Context.Add(systemAccount);
                await WebManager.Instance().Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemAccount);
        }

        // GET: SystemAccounts/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await WebManager.Instance().Context.SystemAccounts.FindAsync(id);
            if (systemAccount == null)
            {
                return NotFound();
            }
            return View(systemAccount);
        }

        // POST: SystemAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        {
            if (id != systemAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    WebManager.Instance().Context.Update(systemAccount);
                    await WebManager.Instance().Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemAccountExists(systemAccount.AccountId))
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
            return View(systemAccount);
        }

        // GET: SystemAccounts/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await WebManager.Instance().Context.SystemAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        // POST: SystemAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var systemAccount = await WebManager.Instance().Context.SystemAccounts.FindAsync(id);
            if (systemAccount != null)
            {
                WebManager.Instance().Context.SystemAccounts.Remove(systemAccount);
            }

            await WebManager.Instance().Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemAccountExists(short id)
        {
            return WebManager.Instance().Context.SystemAccounts.Any(e => e.AccountId == id);
        }


    }
}
