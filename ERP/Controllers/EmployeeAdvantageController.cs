using ERP.Models;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using Microsoft.AspNetCore.Mvc;
using ERP.Services.IServiceContracts;

namespace ERP.Controllers
{
    public class EmployeeAdvantageController : Controller
    {
        private readonly IAdvantageService _advantageService;
        public EmployeeAdvantageController(IAdvantageService advantageService)
        {
            _advantageService = advantageService;
        }

        public async Task<IActionResult> Index()
        {
            var advantages = await _advantageService.GetAllAdvantagesAsync();
            return View(advantages);
        }

        public async Task<IActionResult> Details(int id)
        {
            var advantage = await _advantageService.GetAdvantageByIdAsync(id);
            if (advantage == null)
            {
                return NotFound();
            }
            return View(advantage);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeAdvantage advantage)
        {
            if (ModelState.IsValid)
            {
                await _advantageService.SaveAdvantageAsync(advantage);
                return RedirectToAction(nameof(Index));
            }
            return View(advantage); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var advantage = await _advantageService.GetAdvantageByIdAsync(id);
            if (advantage == null)
            {
                return NotFound();
            }
            return View(advantage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeAdvantage advantage)
        {
            if (ModelState.IsValid)
            {
                var updatedAdvantage = await _advantageService.UpdateAdvantageAsync(advantage);
                if (updatedAdvantage == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(advantage);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var advantage = await _advantageService.GetAdvantageByIdAsync(id);
            if (advantage == null)
            {
                return NotFound();
            }
            return View(advantage);
        }   

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _advantageService.DeleteAdvantageAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
} 