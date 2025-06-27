using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPC.Data;
using EPC.Models;
using EPC.Models.ViewModels;
using System.Diagnostics;

namespace EPC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedProducts = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive && p.AverageRating >= 4.5)
                    .OrderByDescending(p => p.AverageRating)
                    .Take(8)
                    .ToListAsync(),
                    
                Categories = await _context.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync(),
                    
                LatestProducts = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive)
                    .OrderByDescending(p => p.CreatedDate)
                    .Take(4)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}