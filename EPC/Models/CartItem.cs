using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPC.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        public int ProductId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        
        public DateTime DateAdded { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual ApplicationUser? User { get; set; }
        
        [NotMapped]
        public decimal TotalPrice => Product?.Price * Quantity ?? 0;
    }
}