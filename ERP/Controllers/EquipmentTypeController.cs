using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using ERP.Models;

namespace ERP.Controllers
{
    public class EquipmentTypeController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentType
        public async Task<IActionResult> Index()
        {
            var types = await _context.EquipmentTypes
                .Include(t => t.Equipments)
                .OrderBy(t => t.Category)
                .ThenBy(t => t.Name)
                .ToListAsync();
            return View(types);
        }

        // GET: EquipmentType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentTypes
                .Include(t => t.Equipments)
                .FirstOrDefaultAsync(m => m.EquipmentTypeId == id);

            if (equipmentType == null)
            {
                return NotFound();
            }

            return View(equipmentType);
        }

        // GET: EquipmentType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EquipmentType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentTypeId,Name,Description,Category,EstimatedLifespanMonths,RequiresMaintenance")] EquipmentType equipmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentType);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Type d'équipement '{equipmentType.Name}' créé avec succès.";
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentType);
        }

        // GET: EquipmentType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentTypes.FindAsync(id);
            if (equipmentType == null)
            {
                return NotFound();
            }
            return View(equipmentType);
        }

        // POST: EquipmentType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipmentTypeId,Name,Description,Category,EstimatedLifespanMonths,RequiresMaintenance")] EquipmentType equipmentType)
        {
            if (id != equipmentType.EquipmentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentType);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Type d'équipement '{equipmentType.Name}' modifié avec succès.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentTypeExists(equipmentType.EquipmentTypeId))
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
            return View(equipmentType);
        }

        // GET: EquipmentType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentTypes
                .Include(t => t.Equipments)
                .FirstOrDefaultAsync(m => m.EquipmentTypeId == id);

            if (equipmentType == null)
            {
                return NotFound();
            }

            return View(equipmentType);
        }

        // POST: EquipmentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentType = await _context.EquipmentTypes
                .Include(t => t.Equipments)
                .FirstOrDefaultAsync(t => t.EquipmentTypeId == id);

            if (equipmentType != null)
            {
                if (equipmentType.Equipments != null && equipmentType.Equipments.Any())
                {
                    TempData["ErrorMessage"] = "Impossible de supprimer ce type car des équipements y sont associés.";
                    return RedirectToAction(nameof(Index));
                }

                _context.EquipmentTypes.Remove(equipmentType);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Type d'équipement '{equipmentType.Name}' supprimé avec succès.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentTypeExists(int id)
        {
            return _context.EquipmentTypes.Any(e => e.EquipmentTypeId == id);
        }
    }
}
