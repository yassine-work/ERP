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
    public class EquipmentController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Equipment
        public async Task<IActionResult> Index(string searchString, int? typeId, EquipmentStatus? status)
        {
            var query = _context.Equipments
                .Include(e => e.EquipmentType)
                .Include(e => e.Assignments)
                    .ThenInclude(a => a.Employee)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(e => e.Name.Contains(searchString) || 
                                         e.SerialNumber.Contains(searchString) ||
                                         e.InventoryCode.Contains(searchString) ||
                                         e.Brand.Contains(searchString));
            }

            if (typeId.HasValue)
            {
                query = query.Where(e => e.EquipmentTypeId == typeId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(e => e.Status == status.Value);
            }

            ViewBag.EquipmentTypes = await _context.EquipmentTypes.OrderBy(t => t.Name).ToListAsync();
            ViewBag.SearchString = searchString;
            ViewBag.TypeId = typeId;
            ViewBag.Status = status;

            var equipments = await query.OrderBy(e => e.EquipmentType.Category)
                                       .ThenBy(e => e.Name)
                                       .ToListAsync();
            return View(equipments);
        }

        // GET: Equipment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.EquipmentType)
                .Include(e => e.Assignments)
                    .ThenInclude(a => a.Employee)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);

            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipment/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.EquipmentTypes = await _context.EquipmentTypes.OrderBy(t => t.Name).ToListAsync();
            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,Name,SerialNumber,InventoryCode,EquipmentTypeId,Brand,Model,AcquisitionDate,PurchasePrice,Supplier,WarrantyEndDate,Status,Notes")] Equipment equipment)
        {
            ModelState.Remove("EquipmentType");
            ModelState.Remove("Assignments");

            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Équipement '{equipment.Name}' créé avec succès.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EquipmentTypes = await _context.EquipmentTypes.OrderBy(t => t.Name).ToListAsync();
            return View(equipment);
        }

        // GET: Equipment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            ViewBag.EquipmentTypes = await _context.EquipmentTypes.OrderBy(t => t.Name).ToListAsync();
            return View(equipment);
        }

        // POST: Equipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipmentId,Name,SerialNumber,InventoryCode,EquipmentTypeId,Brand,Model,AcquisitionDate,PurchasePrice,Supplier,WarrantyEndDate,Status,Notes")] Equipment equipment)
        {
            if (id != equipment.EquipmentId)
            {
                return NotFound();
            }

            ModelState.Remove("EquipmentType");
            ModelState.Remove("Assignments");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Équipement '{equipment.Name}' modifié avec succès.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.EquipmentId))
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

            ViewBag.EquipmentTypes = await _context.EquipmentTypes.OrderBy(t => t.Name).ToListAsync();
            return View(equipment);
        }

        // GET: Equipment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.EquipmentType)
                .Include(e => e.Assignments)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);

            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipments
                .Include(e => e.Assignments)
                .FirstOrDefaultAsync(e => e.EquipmentId == id);

            if (equipment != null)
            {
                // Check if equipment is currently assigned
                if (equipment.Assignments != null && equipment.Assignments.Any(a => a.ReturnDate == null))
                {
                    TempData["ErrorMessage"] = "Impossible de supprimer cet équipement car il est actuellement attribué à un employé.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Équipement '{equipment.Name}' supprimé avec succès.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Equipment/Assign/5
        public async Task<IActionResult> Assign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.EquipmentType)
                .FirstOrDefaultAsync(e => e.EquipmentId == id);

            if (equipment == null)
            {
                return NotFound();
            }

            if (equipment.Status != EquipmentStatus.Available)
            {
                TempData["ErrorMessage"] = "Cet équipement n'est pas disponible pour attribution.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Equipment = equipment;
            ViewBag.Employees = await _context.Employes
                .Include(e => e.Poste)
                .OrderBy(e => e.Nom)
                .ThenBy(e => e.Prenom)
                .ToListAsync();

            return View(new EquipmentAssignment { EquipmentId = equipment.EquipmentId, AssignmentDate = DateTime.Today });
        }

        // POST: Equipment/Assign/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int id, [Bind("EquipmentId,EmployeeId,AssignmentDate,ConditionAtAssignment,AssignedBy,AssignmentNotes")] EquipmentAssignment assignment)
        {
            if (id != assignment.EquipmentId)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            ModelState.Remove("Equipment");
            ModelState.Remove("Employee");

            if (ModelState.IsValid)
            {
                // Update equipment status
                equipment.Status = EquipmentStatus.Assigned;
                _context.Update(equipment);

                // Create assignment
                _context.EquipmentAssignments.Add(assignment);
                await _context.SaveChangesAsync();

                var employee = await _context.Employes.FindAsync(assignment.EmployeeId);
                TempData["SuccessMessage"] = $"Équipement '{equipment.Name}' attribué à {employee?.Prenom} {employee?.Nom} avec succès.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Equipment = equipment;
            ViewBag.Employees = await _context.Employes
                .Include(e => e.Poste)
                .OrderBy(e => e.Nom)
                .ThenBy(e => e.Prenom)
                .ToListAsync();

            return View(assignment);
        }

        // GET: Equipment/Return/5
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.EquipmentType)
                .Include(e => e.Assignments)
                    .ThenInclude(a => a.Employee)
                .FirstOrDefaultAsync(e => e.EquipmentId == id);

            if (equipment == null)
            {
                return NotFound();
            }

            var activeAssignment = equipment.Assignments?.FirstOrDefault(a => a.ReturnDate == null);
            if (activeAssignment == null)
            {
                TempData["ErrorMessage"] = "Cet équipement n'est actuellement pas attribué.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Equipment = equipment;
            activeAssignment.ReturnDate = DateTime.Today;
            return View(activeAssignment);
        }

        // POST: Equipment/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id, int assignmentId, DateTime returnDate, AssignmentCondition conditionAtReturn, string? returnNotes)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            var assignment = await _context.EquipmentAssignments
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);

            if (assignment == null)
            {
                return NotFound();
            }

            // Update assignment
            assignment.ReturnDate = returnDate;
            assignment.ConditionAtReturn = conditionAtReturn;
            assignment.ReturnNotes = returnNotes;
            _context.Update(assignment);

            // Update equipment status based on condition
            equipment.Status = conditionAtReturn == AssignmentCondition.Poor 
                ? EquipmentStatus.InMaintenance 
                : EquipmentStatus.Available;
            _context.Update(equipment);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Équipement '{equipment.Name}' retourné par {assignment.Employee?.Prenom} {assignment.Employee?.Nom} avec succès.";
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipments.Any(e => e.EquipmentId == id);
        }
    }
}
