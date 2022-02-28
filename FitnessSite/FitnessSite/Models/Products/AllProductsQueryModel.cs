namespace FitnessSite.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 3;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalProducts { get; set; }

        public string Type { get; set; }

        public IEnumerable<string> Types { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
