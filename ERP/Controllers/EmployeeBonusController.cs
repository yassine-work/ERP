using ERP.Models;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using Microsoft.AspNetCore.Mvc;
using ERP.Services.IServiceContracts;

namespace ERP.Controllers
{
    public class EmployeeBonusController : Controller
    {
        private readonly IBonusService _bonusService;
        public EmployeeBonusController(IBonusService bonusService)
        {
            _bonusService = bonusService;
        }

        public async Task<IActionResult> Index()
        {
            var bonuses = await _bonusService.GetAllBonusesAsync();
            return View(bonuses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bonus = await _bonusService.GetBonusByIdAsync(id);
            if (bonus == null)
            {
                return NotFound();
            }
            return View(bonus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeBonus bonus)
        {
            if (ModelState.IsValid)
            {
                await _bonusService.SaveBonusAsync(bonus);
                return RedirectToAction(nameof(Index));
            }
            return View(bonus);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var bonus = await _bonusService.GetBonusByIdAsync(id);
            if (bonus == null)
            {
                return NotFound();
            }
            return View(bonus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeBonus bonus)
        {
            if (ModelState.IsValid)
            {
                await _bonusService.UpdateBonusAsync(id, bonus);
                return RedirectToAction(nameof(Index));
            }
            return View(bonus);
        } 

        public async Task<IActionResult> Delete(int id)
        {
            var bonus = await _bonusService.GetBonusByIdAsync(id);
            if (bonus == null)
            {
                return NotFound();
            }
            return View(bonus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bonusService.DeleteBonusAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}