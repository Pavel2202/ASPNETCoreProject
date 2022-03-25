namespace FitnessSite.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Carts;
    using System.Collections.Generic;
    using FitnessSite.Data.Models;

    using static WebConstants;

    public class CartsControllerTest
    {
        [Fact]
        public void MyCartShouldReturnView()
            => MyController<CartsController>
                .Instance(controller => controller
                    .WithData(Product(), Cart())
                    .WithUser())
                .Calling(c => c.MyCart())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<ProductsViewModel>>());

        [Fact]
        public void RemoveShouldReturnRedirect()
            => MyController<CartsController>
                .Instance(controller => controller
                    .WithData(Product(), Cart())
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
                    .WithData(Product(), Cart())
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
                    .WithData(Product(), Cart())
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

        private static Product Product()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Protein",
                Price = 100,
                Type = ProductType.Supplement,
                ImageUrl = "https://www.silabg.com/uf/product/2945_pm_new.jpg",
                Description = "Best protein. Buy only here.",
                CartId = 1
            };

            return product;
        }

        private static Cart Cart()
        {
            var cart = new Cart()
            {
                Id = 1,
                User = new User
                {
                    Id = "TestId"
                }
            };

            return cart;
        }
    }
}
