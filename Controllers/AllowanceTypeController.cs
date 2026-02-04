using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using ERP.Models;

namespace ERP.Controllers
{
    public class AllowanceTypeController : Controller
    {
        private readonly AppDbContext _context;

        public AllowanceTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /AllowanceType
        public async Task<IActionResult> Index()
        {
            var types = await _context.AllowanceTypes.ToListAsync();
            return View(types);
        }

        // GET: /AllowanceType/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var type = await _context.AllowanceTypes
                .FirstOrDefaultAsync(a => a.AllowanceTypeId == id);

            if (type == null) return NotFound();

            return View(type);
        }

        // GET: /AllowanceType/Create
        public IActionResult Create(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /AllowanceType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AllowanceType type, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(type);
            }

            _context.AllowanceTypes.Add(type);
            await _context.SaveChangesAsync();
            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /AllowanceType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var type = await _context.AllowanceTypes.FindAsync(id);
            if (type == null) return NotFound();

            return View(type);
        }

        // POST: /AllowanceType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AllowanceType type)
        {
            if (id != type.AllowanceTypeId) return BadRequest();
            if (!ModelState.IsValid) return View(type);

            _context.Update(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /AllowanceType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.AllowanceTypes
                .FirstOrDefaultAsync(a => a.AllowanceTypeId == id);

            if (type == null) return NotFound();

            return View(type);
        }

        // POST: /AllowanceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type = await _context.AllowanceTypes.FindAsync(id);
            if (type == null) return NotFound();

            _context.AllowanceTypes.Remove(type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
