namespace FitnessSite.Test.Admin.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Products/All")
                .To<ProductsController>(c => c.All(With.Any<string>()));

        [Fact]
        public void ChangeVisibilityShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Products/ChangeVisibility")
                .To<ProductsController>(c => c.ChangeVisibility(With.Any<int>()));
    }
}
