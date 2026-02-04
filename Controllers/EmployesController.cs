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
    public class EmployesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employes
        public async Task<IActionResult> Index()
        {
            var employes = await _context.Employes
                .Include(e => e.Poste)
                .ToListAsync();
            return View(employes);
        }

        // GET: Employes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.Poste)
                .Include(e => e.CompensationPackages)
                .Include(e => e.EquipmentAssignments)
                    .ThenInclude(a => a.Equipment)
                        .ThenInclude(eq => eq.EquipmentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        // GET: Employes/Create
        public async Task<IActionResult> Create()
        {
            // Load postes for dropdown
            ViewBag.Postes = await _context.Postes
                .OrderBy(p => p.Department)
                .ThenBy(p => p.Title)
                .ToListAsync();
            
            // If a new poste was just created, pre-select it
            if (TempData["NewPosteId"] != null)
            {
                ViewBag.SelectedPosteId = (int)TempData["NewPosteId"];
                TempData["SuccessMessage"] = "Poste created successfully! You can now create the employee.";
            }
            
            return View();
        }

        // POST: Employes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Matricule,Nom,Prenom,Gender,DateNaissance,LieuNaissance,CIN,Nationalite,SituationFamiliale,NombreEnfants,Email,Telephone,TelephoneUrgence,ContactUrgence,Adresse,Ville,CodePostal,PosteId,DateEmbauche,DateFinContrat,TypeContrat,Status,PhotoUrl,Notes")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                // Get the Poste to retrieve minimum salary
                var poste = await _context.Postes.FindAsync(employe.PosteId);
                
                if (poste == null)
                {
                    ModelState.AddModelError("PosteId", "Selected poste not found.");
                    ViewBag.Postes = await _context.Postes
                        .OrderBy(p => p.Department)
                        .ThenBy(p => p.Title)
                        .ToListAsync();
                    return View(employe);
                }

                // Add employee first to generate Id
                _context.Add(employe);
                await _context.SaveChangesAsync();

                // Create default compensation package based on poste minimum salary
                var defaultPackage = new CompensationPackage
                {
                    EmployeeId = employe.Id,
                    BaseSalary = poste.MinimumBaseSalary,
                    EffectiveFrom = employe.DateEmbauche,
                    IsActive = true,
                    EffectiveTo = null
                };

                _context.CompensationPackages.Add(defaultPackage);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Employee '{employe.Nom} {employe.Prenom}' created successfully with default compensation package (Base Salary: {poste.MinimumBaseSalary:C})";
                
                return RedirectToAction(nameof(Index));
            }
            
            // Reload postes if validation fails
            ViewBag.Postes = await _context.Postes
                .OrderBy(p => p.Department)
                .ThenBy(p => p.Title)
                .ToListAsync();
            
            return View(employe);
        }

        // GET: Employes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.Poste)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (employe == null)
            {
                return NotFound();
            }
            
            // Load postes for dropdown
            ViewBag.Postes = await _context.Postes
                .OrderBy(p => p.Department)
                .ThenBy(p => p.Title)
                .ToListAsync();
            
            return View(employe);
        }

        

                   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricule,Nom,Prenom,Gender,DateNaissance,LieuNaissance,CIN,Nationalite,SituationFamiliale,NombreEnfants,Email,Telephone,TelephoneUrgence,ContactUrgence,Adresse,Ville,CodePostal,PosteId,DateEmbauche,DateFinContrat,TypeContrat,Status,PhotoUrl,Notes")] Employe employe)
        {
            if (id != employe.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Poste");
            ModelState.Remove("CompensationPackages");
            ModelState.Remove("EquipmentAssignments");

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if poste changed
                    var originalEmploye = await _context.Employes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
                    
                    bool posteChanged = originalEmploye.PosteId != employe.PosteId;
                    
                    // Update employee
                    _context.Update(employe);
                    await _context.SaveChangesAsync();
                    
                    // If poste changed, handle compensation package update
                    if (posteChanged)
                    {
                        // Get the new poste
                        var newPoste = await _context.Postes.FindAsync(employe.PosteId);
                        
                        if (newPoste != null)
                        {
                            // 1. Deactivate current active package
                            var activePackage = await _context.CompensationPackages
                                .FirstOrDefaultAsync(cp => cp.EmployeeId == employe.Id && cp.IsActive);
                            
                            if (activePackage != null)
                            {
                                activePackage.IsActive = false;
                                activePackage.EffectiveTo = DateTime.Now;
                                _context.Update(activePackage);
                            }
                            
                            // 2. Create new compensation package with new poste's minimum salary
                            var newPackage = new CompensationPackage
                            {
                                EmployeeId = employe.Id,
                                BaseSalary = newPoste.MinimumBaseSalary,
                                EffectiveFrom = DateTime.Now,
                                IsActive = true,
                                EffectiveTo = null
                            };
                            
                            _context.CompensationPackages.Add(newPackage);
                            await _context.SaveChangesAsync();
                            
                            TempData["SuccessMessage"] = $"Employee's poste updated from {originalEmploye.PosteId} to {newPoste.Title}. New compensation package created with base salary: {newPoste.MinimumBaseSalary:C}";
                        }
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Employee updated successfully.";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeExists(employe.Id))
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
            
            // Reload postes if validation fails
            ViewBag.Postes = await _context.Postes
                .OrderBy(p => p.Department)
                .ThenBy(p => p.Title)
                .ToListAsync();
            
            return View(employe);
        }

        // GET: Employes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.Poste)
                .Include(e => e.CompensationPackages)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employe = await _context.Employes
                .Include(e => e.CompensationPackages)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (employe != null)
            {
                // Delete related compensation packages first (cascade should handle this, but being explicit)
                if (employe.CompensationPackages != null && employe.CompensationPackages.Any())
                {
                    _context.CompensationPackages.RemoveRange(employe.CompensationPackages);
                }
                
                _context.Employes.Remove(employe);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Employee and related compensation packages deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeExists(int id)
        {
            return _context.Employes.Any(e => e.Id == id);
        }
    }
}
