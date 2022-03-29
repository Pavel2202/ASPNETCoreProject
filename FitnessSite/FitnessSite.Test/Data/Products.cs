namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using FitnessSite.Data.Models.Enums;
    using FitnessSite.Models.Products;
    using System.Collections.Generic;
    using System.Linq;

    public static class Products
    {
        public static IEnumerable<Product> TenPublicProducts
            => Enumerable.Range(0, 10).Select(p => new Product
            {
                IsPublic = true
            });

        public static AllProductsQueryModel GetQuery
            => new AllProductsQueryModel
            {
                SearchTerm = null,
                Type = null,
                Sorting = ProductSorting.DateCreated,
                CurrentPage = 1
            };

        public static Product Product
            => new Product()
            {
                Id = 1,
                Name = "Protein",
                Price = 100,
                ImageUrl = "https://www.silabg.com/uf/product/2945_pm_new.jpg",
                Type = ProductType.Supplement,
                Description = "Best protein. Buy only here.",
                IsPublic = true
            };

        public static Cart Cart
            => new Cart()
            {
                User = new User
                {
                    Id = "TestId"
                }
            };
    }
}
