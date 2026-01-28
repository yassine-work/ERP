using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using ERP.Models;

namespace ERP.Controllers
{
    public class BonusTypeController : Controller
    {
        private readonly AppDbContext _context;

        public BonusTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /BonusType
        public async Task<IActionResult> Index()
        {
            var types = await _context.BonusTypes.ToListAsync();
            return View(types);
        }

        // GET: /BonusType/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var type = await _context.BonusTypes
                .FirstOrDefaultAsync(b => b.BonusTypeId == id);

            if (type == null) return NotFound();

            return View(type);
        }

        // GET: /BonusType/Create
        public IActionResult Create(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /BonusType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BonusType type, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(type);
            }

            _context.BonusTypes.Add(type);
            await _context.SaveChangesAsync();
            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /BonusType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var type = await _context.BonusTypes.FindAsync(id);
            if (type == null) return NotFound();

            return View(type);
        }

        // POST: /BonusType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BonusType type)
        {
            if (id != type.BonusTypeId) return BadRequest();
            if (!ModelState.IsValid) return View(type);

            _context.Update(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /BonusType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.BonusTypes
                .FirstOrDefaultAsync(b => b.BonusTypeId == id);

            if (type == null) return NotFound();

            return View(type);
        }

        // POST: /BonusType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type = await _context.BonusTypes.FindAsync(id);
            if (type == null) return NotFound();

            _context.BonusTypes.Remove(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
