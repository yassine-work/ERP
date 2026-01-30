using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using ERP.Models;

namespace ERP.Controllers
{
    public class AdvantageTypeController : Controller
    {
        private readonly AppDbContext _context;

        public AdvantageTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /AdvantageType
        public async Task<IActionResult> Index()
        {
            var types = await _context.AdvantageTypes.ToListAsync();
            return View(types);
        }

        // GET: /AdvantageType/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var type = await _context.AdvantageTypes.FirstOrDefaultAsync(a => a.AdvantageTypeId == id);
            if (type == null) return NotFound();

            return View(type);
        }

        // GET: /AdvantageType/Create
        public IActionResult Create(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /AdvantageType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvantageType type, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(type);
            }

            _context.AdvantageTypes.Add(type);
            await _context.SaveChangesAsync();
            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /AdvantageType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var type = await _context.AdvantageTypes.FindAsync(id);
            if (type == null) return NotFound();

            return View(type);
        }

        // POST: /AdvantageType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdvantageType type)
        {
            if (id != type.AdvantageTypeId) return BadRequest();
            if (!ModelState.IsValid) return View(type);

            _context.Update(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /AdvantageType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.AdvantageTypes.FirstOrDefaultAsync(a => a.AdvantageTypeId == id);
            if (type == null) return NotFound();

            return View(type);
        }

        // POST: /AdvantageType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type = await _context.AdvantageTypes.FindAsync(id);
            if (type == null) return NotFound();

            _context.AdvantageTypes.Remove(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
