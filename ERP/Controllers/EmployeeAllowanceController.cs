using ERP.Models;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using Microsoft.AspNetCore.Mvc;
using ERP.Services.IServiceContracts;

namespace ERP.Controllers
{
    public class EmployeeAllowanceController : Controller
    {
        private readonly IAllowanceService _allowanceService;
        public EmployeeAllowanceController(IAllowanceService allowanceService)
        {
            _allowanceService = allowanceService;
        }

        public async Task<IActionResult> Index()
        {
            var allowances = await _allowanceService.GetAllAllowancesAsync();
            return View(allowances);
        }

        public async Task<IActionResult> Details(int id)
        {
            var allowance = await _allowanceService.GetAllowanceByIdAsync(id);
            if (allowance == null)
            {
                return NotFound();
            }
            return View(allowance);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeAllowance allowance)
        {
            if (ModelState.IsValid)
            {
                await _allowanceService.SaveAllowanceAsync(allowance);
                return RedirectToAction(nameof(Index));
            }
            return View(allowance); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var allowance = await _allowanceService.GetAllowanceByIdAsync(id);
            if (allowance == null)
            {
                return NotFound();
            }
            return View(allowance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeAllowance allowance)
        {
            if (ModelState.IsValid)
            {
                var updatedAllowance = await _allowanceService.UpdateAllowanceAsync(allowance);
                if (updatedAllowance == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(allowance);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var allowance = await _allowanceService.GetAllowanceByIdAsync(id);
            if (allowance == null)
            {
                return NotFound();
            }
            return View(allowance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _allowanceService.DeleteAllowanceAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}