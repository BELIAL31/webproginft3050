namespace EPC.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Product> LatestProducts { get; set; } = new List<Product>();
    }
}