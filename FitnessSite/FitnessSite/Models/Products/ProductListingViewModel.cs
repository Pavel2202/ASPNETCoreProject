namespace FitnessSite.Models.Products
{
    public class ProductListingViewModel : IProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
