using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EPC.Models;

namespace EPC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configure relationships
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<CartItem>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data to match your existing database
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Processors", Description = "CPUs and processors for high-performance computing" },
                new Category { Id = 2, Name = "Graphics Cards", Description = "GPUs and video cards for gaming and professional work" },
                new Category { Id = 3, Name = "Memory", Description = "RAM and memory modules for system performance" },
                new Category { Id = 4, Name = "Storage", Description = "SSDs, HDDs, and storage devices for data storage" },
                new Category { Id = 5, Name = "Motherboards", Description = "Motherboards and chipsets for system foundation" },
                new Category { Id = 6, Name = "Power Supplies", Description = "PSUs and power supplies for stable power delivery" },
                new Category { Id = 7, Name = "Cases", Description = "PC cases and enclosures for system protection" },
                new Category { Id = 8, Name = "Cooling", Description = "Fans, coolers, and thermal solutions for temperature management" }
            );

            // Seed Products
            builder.Entity<Product>().HasData(
                new Product 
                { 
                    Id = 1, 
                    Name = "AMD Ryzen 9 7950X", 
                    Description = "16-Core, 32-Thread Unlocked Desktop Processor",
                    Price = 699.99m, 
                    CategoryId = 1, 
                    StockQuantity = 50,
                    AverageRating = 4.8,
                    ReviewCount = 245,
                    ImageUrl = "/images/products/ryzen-9-7950x.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 2, 
                    Name = "NVIDIA GeForce RTX 4090", 
                    Description = "24GB GDDR6X Graphics Card",
                    Price = 1599.99m, 
                    CategoryId = 2, 
                    StockQuantity = 25,
                    AverageRating = 4.9,
                    ReviewCount = 189,
                    ImageUrl = "/images/products/rtx-4090.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 3, 
                    Name = "Corsair Vengeance DDR5", 
                    Description = "32GB (2x16GB) DDR5-5600 C36 Memory Kit",
                    Price = 299.99m, 
                    CategoryId = 3, 
                    StockQuantity = 75,
                    AverageRating = 4.7,
                    ReviewCount = 156,
                    ImageUrl = "/images/products/corsair-vengeance-ddr5.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 4, 
                    Name = "Samsung 980 PRO SSD", 
                    Description = "2TB NVMe M.2 Internal SSD",
                    Price = 199.99m, 
                    CategoryId = 4, 
                    StockQuantity = 100,
                    AverageRating = 4.6,
                    ReviewCount = 312,
                    ImageUrl = "/images/products/samsung-980-pro.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 5, 
                    Name = "ASUS ROG STRIX X670E-E", 
                    Description = "AM5 ATX Gaming Motherboard",
                    Price = 449.99m, 
                    CategoryId = 5, 
                    StockQuantity = 30,
                    AverageRating = 4.5,
                    ReviewCount = 89,
                    ImageUrl = "/images/products/asus-rog-strix-x670e.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 6, 
                    Name = "Corsair RM850x", 
                    Description = "850W 80+ Gold Certified PSU",
                    Price = 139.99m, 
                    CategoryId = 6, 
                    StockQuantity = 60,
                    AverageRating = 4.8,
                    ReviewCount = 203,
                    ImageUrl = "/images/products/corsair-rm850x.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 7, 
                    Name = "NZXT H7 Flow", 
                    Description = "Mid-Tower ATX PC Case",
                    Price = 129.99m, 
                    CategoryId = 7, 
                    StockQuantity = 40,
                    AverageRating = 4.4,
                    ReviewCount = 167,
                    ImageUrl = "/images/products/nzxt-h7-flow.jpg",
                    IsActive = true
                },
                new Product 
                { 
                    Id = 8, 
                    Name = "Noctua NH-D15", 
                    Description = "Premium CPU Cooler",
                    Price = 99.99m, 
                    CategoryId = 8, 
                    StockQuantity = 80,
                    AverageRating = 4.9,
                    ReviewCount = 278,
                    ImageUrl = "/images/products/noctua-nh-d15.jpg",
                    IsActive = true
                }
            );
        }
    }
}