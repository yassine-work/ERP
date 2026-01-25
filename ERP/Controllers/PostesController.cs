using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Data; 

namespace ERP.Controllers
{
    public class PosteController : Controller
    {
        private readonly AppDbContext _context;

        public PosteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Poste
        public async Task<IActionResult> Index()
        {
            var postes = await _context.Postes
                .Include(p => p.Employees)
                .OrderBy(p => p.Department)
                .ThenBy(p => p.Title)
                .ToListAsync();
            
            return View(postes);
        }

        // GET: Poste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var poste = await _context.Postes
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(m => m.PosteId == id);

            if (poste == null)
                return NotFound();

            return View(poste);
        }

        // GET: Poste/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poste/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Department,MinimumBaseSalary,Description")] Poste poste)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicates
                var exists = await _context.Postes
                    .AnyAsync(p => p.Title == poste.Title && p.Department == poste.Department);

                if (exists)
                {
                    ModelState.AddModelError("", "A poste with this title already exists in this department.");
                    return View(poste);
                }

                _context.Add(poste);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Poste created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(poste);
        }

        // GET: Poste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var poste = await _context.Postes.FindAsync(id);
            if (poste == null)
                return NotFound();

            return View(poste);
        }

        // POST: Poste/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PosteId,Title,Department,MinimumBaseSalary,Description")] Poste poste)
        {
            if (id != poste.PosteId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poste);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Poste updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosteExists(poste.PosteId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(poste);
        }

        // GET: Poste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var poste = await _context.Postes
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(m => m.PosteId == id);

            if (poste == null)
                return NotFound();

            return View(poste);
        }

        // POST: Poste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poste = await _context.Postes
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.PosteId == id);

            if (poste == null)
                return NotFound();

            // Check if poste has employees
            if (poste.Employees != null && poste.Employees.Any())
            {
                TempData["ErrorMessage"] = $"Cannot delete this poste. It has {poste.Employees.Count} employee(s) assigned.";
                return RedirectToAction(nameof(Index));
            }

            _context.Postes.Remove(poste);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Poste deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool PosteExists(int id)
        {
            return _context.Postes.Any(e => e.PosteId == id);
        }
    }
}