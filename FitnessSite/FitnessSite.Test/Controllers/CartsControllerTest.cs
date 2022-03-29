namespace FitnessSite.Test.Controllers
{
    using FitnessSite.Controllers;
    using FitnessSite.Models.Carts;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;
    using static Data.Carts;
    using static WebConstants;

    public class CartsControllerTest
    {
        [Fact]
        public void MyCartShouldReturnView()
            => MyController<CartsController>
                .Instance(controller => controller
                    .WithData(Product)
                    .WithData(Cart)
                    .WithUser())
                .Calling(c => c.MyCart())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<ProductViewModel>>());

        [Fact]
        public void RemoveShouldReturnRedirect()
            => MyController<CartsController>
                .Instance(controller => controller
                    .WithData(Product)
                    .WithData(Cart)
                    .WithUser())
                .Calling(c => c.Remove(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CartsController>(c => c.MyCart()));

        [Fact]
        public void BuyShouldReturnRedirect()
            => MyController<CartsController>
                .Instance(controller => controller
                    .WithData(Product)
                    .WithData(Cart)
                    .WithUser())
                .Calling(c => c.Buy())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CartsController>(c => c.MyCart()));

        [Fact]
        public void ClearShouldReturnRedirect()
            => MyController<CartsController>
                .Instance(controller => controller
                    .WithData(Product)
                    .WithData(Cart)
                    .WithUser())
                .Calling(c => c.Clear())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CartsController>(c => c.MyCart()));
    }
}
