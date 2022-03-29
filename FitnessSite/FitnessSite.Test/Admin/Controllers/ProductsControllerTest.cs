namespace FitnessSite.Test.Admin.Controllers
{
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Products;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Products;

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
                    .WithData(Product))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();
    }
}
