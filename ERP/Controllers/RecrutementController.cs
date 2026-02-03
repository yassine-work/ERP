using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using ERP.Models;

namespace ERP.Controllers
{
    public class RecrutementController : Controller
    {
        private readonly AppDbContext _context;

        public RecrutementController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Recrutement - Dashboard
        public async Task<IActionResult> Index()
        {
            ViewBag.JobOfferCount = await _context.JobOffers.CountAsync();
            ViewBag.PublishedOfferCount = await _context.JobOffers.Where(j => j.Status == JobOfferStatus.Published).CountAsync();
            ViewBag.CandidatCount = await _context.Candidats.CountAsync();
            ViewBag.CandidatureCount = await _context.Candidatures.CountAsync();
            ViewBag.PendingCandidatures = await _context.Candidatures.Where(c => c.Status == CandidatureStatus.Submitted || c.Status == CandidatureStatus.UnderReview).CountAsync();
            
            var recentJobOffers = await _context.JobOffers
                .Include(j => j.Poste)
                .OrderByDescending(j => j.CreatedAt)
                .Take(5)
                .ToListAsync();

            var recentCandidatures = await _context.Candidatures
                .Include(c => c.Candidat)
                .Include(c => c.JobOffer)
                .OrderByDescending(c => c.DateCandidature)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentJobOffers = recentJobOffers;
            ViewBag.RecentCandidatures = recentCandidatures;

            return View();
        }

        #region Job Offers

        // GET: Recrutement/Offres
        public async Task<IActionResult> Offres(string? status)
        {
            var query = _context.JobOffers.Include(j => j.Poste).AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<JobOfferStatus>(status, out var statusEnum))
            {
                query = query.Where(j => j.Status == statusEnum);
            }

            var offers = await query.OrderByDescending(j => j.CreatedAt).ToListAsync();
            return View(offers);
        }

        // GET: Recrutement/CreateOffre
        public IActionResult CreateOffre()
        {
            ViewBag.Postes = new SelectList(_context.Postes, "PosteId", "Title");
            return View();
        }

        // POST: Recrutement/CreateOffre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOffre(JobOffer jobOffer)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Poste");
            ModelState.Remove("Candidatures");

            if (ModelState.IsValid)
            {
                jobOffer.CreatedAt = DateTime.Now;
                _context.Add(jobOffer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Offre d'emploi créée avec succès!";
                return RedirectToAction(nameof(Offres));
            }
            ViewBag.Postes = new SelectList(_context.Postes, "PosteId", "Title", jobOffer.PosteId);
            return View(jobOffer);
        }

        // GET: Recrutement/EditOffre/5
        public async Task<IActionResult> EditOffre(int? id)
        {
            if (id == null) return NotFound();

            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null) return NotFound();

            ViewBag.Postes = new SelectList(_context.Postes, "PosteId", "Title", jobOffer.PosteId);
            return View(jobOffer);
        }

        // POST: Recrutement/EditOffre/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOffre(int id, JobOffer jobOffer)
        {
            if (id != jobOffer.Id) return NotFound();

            // Remove navigation properties from validation
            ModelState.Remove("Poste");
            ModelState.Remove("Candidatures");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOffer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Offre d'emploi mise à jour avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOfferExists(jobOffer.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Offres));
            }
            ViewBag.Postes = new SelectList(_context.Postes, "PosteId", "Title", jobOffer.PosteId);
            return View(jobOffer);
        }

        // POST: Recrutement/PublishOffre/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublishOffre(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null) return NotFound();

            jobOffer.Status = JobOfferStatus.Published;
            jobOffer.PublishedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Offre publiée avec succès!";
            return RedirectToAction(nameof(Offres));
        }

        // POST: Recrutement/CloseOffre/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseOffre(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null) return NotFound();

            jobOffer.Status = JobOfferStatus.Closed;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Offre fermée avec succès!";
            return RedirectToAction(nameof(Offres));
        }

        // GET: Recrutement/DetailsOffre/5
        public async Task<IActionResult> DetailsOffre(int? id)
        {
            if (id == null) return NotFound();

            var jobOffer = await _context.JobOffers
                .Include(j => j.Poste)
                .Include(j => j.Candidatures!)
                    .ThenInclude(c => c.Candidat)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobOffer == null) return NotFound();

            return View(jobOffer);
        }

        // POST: Recrutement/DeleteOffre/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOffre(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer != null)
            {
                _context.JobOffers.Remove(jobOffer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Offre supprimée avec succès!";
            }
            return RedirectToAction(nameof(Offres));
        }

        #endregion

        #region Candidats

        // GET: Recrutement/Candidats
        public async Task<IActionResult> Candidats(string? search)
        {
            var query = _context.Candidats.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nom.Contains(search) || c.Prenom.Contains(search) || c.Email.Contains(search));
            }

            var candidats = await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
            return View(candidats);
        }

        // GET: Recrutement/CreateCandidat
        public IActionResult CreateCandidat()
        {
            return View();
        }

        // POST: Recrutement/CreateCandidat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCandidat(Candidat candidat)
        {
            // Remove navigation property from validation
            ModelState.Remove("Candidatures");

            if (ModelState.IsValid)
            {
                candidat.CreatedAt = DateTime.Now;
                _context.Add(candidat);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Candidat créé avec succès!";
                return RedirectToAction(nameof(Candidats));
            }
            return View(candidat);
        }

        // GET: Recrutement/EditCandidat/5
        public async Task<IActionResult> EditCandidat(int? id)
        {
            if (id == null) return NotFound();

            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat == null) return NotFound();

            return View(candidat);
        }

        // POST: Recrutement/EditCandidat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidat(int id, Candidat candidat)
        {
            if (id != candidat.Id) return NotFound();

            // Remove navigation property from validation
            ModelState.Remove("Candidatures");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidat);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Candidat mis à jour avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatExists(candidat.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Candidats));
            }
            return View(candidat);
        }

        // GET: Recrutement/DetailsCandidat/5
        public async Task<IActionResult> DetailsCandidat(int? id)
        {
            if (id == null) return NotFound();

            var candidat = await _context.Candidats
                .Include(c => c.Candidatures!)
                    .ThenInclude(c => c.JobOffer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidat == null) return NotFound();

            return View(candidat);
        }

        // POST: Recrutement/DeleteCandidat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCandidat(int id)
        {
            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat != null)
            {
                _context.Candidats.Remove(candidat);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Candidat supprimé avec succès!";
            }
            return RedirectToAction(nameof(Candidats));
        }

        #endregion

        #region Candidatures

        // GET: Recrutement/Candidatures
        public async Task<IActionResult> Candidatures(string? status, int? jobOfferId)
        {
            var query = _context.Candidatures
                .Include(c => c.Candidat)
                .Include(c => c.JobOffer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<CandidatureStatus>(status, out var statusEnum))
            {
                query = query.Where(c => c.Status == statusEnum);
            }

            if (jobOfferId.HasValue)
            {
                query = query.Where(c => c.JobOfferId == jobOfferId.Value);
            }

            ViewBag.JobOffers = new SelectList(await _context.JobOffers.Where(j => j.Status == JobOfferStatus.Published).ToListAsync(), "Id", "Title");
            var candidatures = await query.OrderByDescending(c => c.DateCandidature).ToListAsync();
            return View(candidatures);
        }

        // GET: Recrutement/CreateCandidature
        public IActionResult CreateCandidature(int? jobOfferId, int? candidatId)
        {
            ViewBag.JobOffers = new SelectList(_context.JobOffers.Where(j => j.Status == JobOfferStatus.Published), "Id", "Title", jobOfferId);
            ViewBag.Candidats = new SelectList(_context.Candidats.Select(c => new { c.Id, NomComplet = c.Prenom + " " + c.Nom }), "Id", "NomComplet", candidatId);
            return View();
        }

        // POST: Recrutement/CreateCandidature
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCandidature(Candidature candidature)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Candidat");
            ModelState.Remove("JobOffer");

            // Check for duplicate
            var exists = await _context.Candidatures.AnyAsync(c => c.CandidatId == candidature.CandidatId && c.JobOfferId == candidature.JobOfferId);
            if (exists)
            {
                ModelState.AddModelError("", "Ce candidat a déjà postulé à cette offre.");
            }

            if (ModelState.IsValid)
            {
                candidature.DateCandidature = DateTime.Now;
                candidature.Status = CandidatureStatus.Submitted;
                _context.Add(candidature);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Candidature créée avec succès!";
                return RedirectToAction(nameof(Candidatures));
            }
            ViewBag.JobOffers = new SelectList(_context.JobOffers.Where(j => j.Status == JobOfferStatus.Published), "Id", "Title", candidature.JobOfferId);
            ViewBag.Candidats = new SelectList(_context.Candidats.Select(c => new { c.Id, NomComplet = c.Prenom + " " + c.Nom }), "Id", "NomComplet", candidature.CandidatId);
            return View(candidature);
        }

        // GET: Recrutement/EditCandidature/5
        public async Task<IActionResult> EditCandidature(int? id)
        {
            if (id == null) return NotFound();

            var candidature = await _context.Candidatures
                .Include(c => c.Candidat)
                .Include(c => c.JobOffer)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (candidature == null) return NotFound();

            return View(candidature);
        }

        // POST: Recrutement/EditCandidature/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCandidature(int id, Candidature candidature)
        {
            if (id != candidature.Id) return NotFound();

            // Remove navigation properties from validation
            ModelState.Remove("Candidat");
            ModelState.Remove("JobOffer");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidature);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Candidature mise à jour avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatureExists(candidature.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Candidatures));
            }
            
            // Re-fetch candidature with includes for the view
            var candidatureWithIncludes = await _context.Candidatures
                .Include(c => c.Candidat)
                .Include(c => c.JobOffer)
                .FirstOrDefaultAsync(c => c.Id == id);
            return View(candidatureWithIncludes ?? candidature);
        }

        // POST: Recrutement/UpdateCandidatureStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCandidatureStatus(int id, CandidatureStatus status)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature == null) return NotFound();

            candidature.Status = status;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Statut mis à jour avec succès!";
            return RedirectToAction(nameof(Candidatures));
        }

        // POST: Recrutement/DeleteCandidature/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCandidature(int id)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature != null)
            {
                _context.Candidatures.Remove(candidature);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Candidature supprimée avec succès!";
            }
            return RedirectToAction(nameof(Candidatures));
        }

        #endregion

        private bool JobOfferExists(int id) => _context.JobOffers.Any(e => e.Id == id);
        private bool CandidatExists(int id) => _context.Candidats.Any(e => e.Id == id);
        private bool CandidatureExists(int id) => _context.Candidatures.Any(e => e.Id == id);
    }
}
