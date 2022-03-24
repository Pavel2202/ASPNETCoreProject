namespace FitnessSite.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Products;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/All")
                .To<ProductsController>(c => c.All(With.Any<AllProductsQueryModel>()));

        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Add")
                .To<ProductsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Products/Add")
                    .WithMethod(HttpMethod.Post))
                .To<ProductsController>(c => c.Add(With.Any<ProductFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Details")
                .To<ProductsController>(c => c.Details(With.Any<int>(), With.Any<string>()));

        [Fact]
        public void GetEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Edit")
                .To<ProductsController>(c => c.Edit(With.Any<int>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Products/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<ProductsController>(c => c.Edit(With.Any<int>(), With.Any<ProductFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Delete")
                .To<ProductsController>(c => c.Delete(With.Any<int>()));

        [Fact]
        public void AddToCartShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/AddToCart")
                .To<ProductsController>(c => c.AddToCart(With.Any<int>()));

    }
}
