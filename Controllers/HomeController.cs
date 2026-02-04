using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Data;

namespace ERP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Fetch statistics for the dashboard
        ViewBag.EmployeeCount = await _context.Employes.CountAsync();
        ViewBag.PosteCount = await _context.Postes.CountAsync();
        ViewBag.EquipmentCount = await _context.Equipments.CountAsync();
        ViewBag.PackageCount = await _context.CompensationPackages.CountAsync();
        
        // Recruitment stats
        ViewBag.JobOfferCount = await _context.JobOffers.Where(j => j.Status == JobOfferStatus.Published).CountAsync();
        ViewBag.CandidatureCount = await _context.Candidatures.CountAsync();
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Health check endpoint for container orchestrators
    /// </summary>
    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}