using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPC.Data;

namespace EPC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId, string? searchTerm, string sortBy = "name")
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p => 
                    p.Name.Contains(searchTerm) || 
                    (p.Description != null && p.Description.Contains(searchTerm)));
            }

            productsQuery = sortBy.ToLower() switch
            {
                "price_asc" => productsQuery.OrderBy(p => p.Price),
                "price_desc" => productsQuery.OrderByDescending(p => p.Price),
                "rating" => productsQuery.OrderByDescending(p => p.AverageRating),
                "newest" => productsQuery.OrderByDescending(p => p.CreatedDate),
                _ => productsQuery.OrderBy(p => p.Name)
            };

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSearch = searchTerm;
            ViewBag.CurrentSort = sortBy;

            return View(await productsQuery.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> PriceList()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Category!.Name)
                .ThenBy(p => p.Name)
                .ToListAsync();

            return View(products);
        }
    }
}