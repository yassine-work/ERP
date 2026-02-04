using Microsoft.EntityFrameworkCore;
using ERP.Services;
using ERP.Services.IServiceContracts;
using ERP.Data;
using ERP.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add HttpContextAccessor for session access
builder.Services.AddHttpContextAccessor();

// Register the sandbox database factory as singleton (manages all sessions)
builder.Services.AddSingleton<SandboxDbContextFactory>(sp => 
    new SandboxDbContextFactory(TimeSpan.FromMinutes(20)));

// Register the scoped provider that creates fresh context per request
builder.Services.AddScoped<SandboxDbContextProvider>();

// Register AppDbContext as Scoped - creates fresh context per request via provider
// The provider caches it within the request, DI disposes it at end of request
builder.Services.AddScoped<AppDbContext>(sp => 
    sp.GetRequiredService<SandboxDbContextProvider>().GetContext());

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICompensationPackageService, CompensationPackageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add sandbox session middleware BEFORE authorization
app.UseSandboxSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Log startup info
Console.WriteLine("===========================================");
Console.WriteLine("  ERP System - SANDBOX MODE ENABLED");
Console.WriteLine("  Each visitor gets their own isolated DB");
Console.WriteLine("  Sessions expire after 20 minutes");
Console.WriteLine("===========================================");

app.Run();