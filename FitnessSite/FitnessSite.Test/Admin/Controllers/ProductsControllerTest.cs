namespace FitnessSite.Test.Admin.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Products;
    using FitnessSite.Data.Models;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.All("1"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllProductsQueryModel>());

        [Fact]
        public void ChangeVisibilityShouldReturnRedirect()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product()))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();

        private static Product Product()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Protein",
                Price = 100,
                ImageUrl = "https://www.silabg.com/uf/product/2945_pm_new.jpg",
                Type = ProductType.Supplement,
                Description = "Best protein. Buy only here.",
                IsPublic = true
            };

            return product;
        }
    }
}
