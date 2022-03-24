namespace FitnessSite.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Products;
    using FitnessSite.Data.Models;
    using System.Linq;

    using static Data.Products;

    using static Areas.Admin.AdminConstants;
    using static WebConstants;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(TenPublicProducts))
                .Calling(c => c.All(GetQuery()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllProductsQueryModel>());

        [Fact]
        public void GetAddShouldBeForAdminsAndReturnView()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Protein",
            100.00,
            "https://www.silabg.com/uf/product/2945_pm_new.jpg",
            "Supplement",
            "Best protein. Buy only here.")]
        public void PostAddShouldBeForAdminsAndReturnRedirectWithValidModel(
            string name,
            decimal price,
            string imageUrl,
            string type,
            string description)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new ProductFormModel
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl,
                    Type = type,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Product>(products => products
                        .Any(p =>
                            p.Name == name &&
                            p.Price == price &&
                            p.ImageUrl == imageUrl &&
                            p.Type == ProductType.Supplement &&
                            p.Description == description)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<AllProductsQueryModel>())));

        [Fact]
        public void DetailsShouldReturnView()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product()))
                .Calling(c => c.Details(1, "Protein"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductDetailsViewModel>());

        [Fact]
        public void GetEditShouldBeForAdminsAndReturnView()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product()))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(1,
            "ProteinSecond",
            100.00,
            "https://www.silabg.com/uf/product/2945_pm_new.jpg",
            "Supplement",
            "Best protein. Buy only here.")]
        public void PostEditShouldBeForAdminsAndReturnRedirectWithModel(
            int id,
            string name,
            decimal price,
            string imageUrl,
            string type,
            string description)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product())
                    .WithUser())
                .Calling(c => c.Edit(id, new ProductFormModel
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl,
                    Type = type,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.Details(id, name)));

        [Fact]
        public void DeleteShouldBeForAdminsAndReturnRedirect()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product())
                    .WithUser())
               .Calling(c => c.Delete(1))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<AllProductsQueryModel>())));

        [Fact]
        public void AddToCartShouldBeAuthorizedAndReturnRedirect()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product(), Cart())
                    .WithUser())
                .Calling(c => c.AddToCart(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<AllProductsQueryModel>())));

        private static AllProductsQueryModel GetQuery()
        {
            AllProductsQueryModel model = new AllProductsQueryModel
            {
                SearchTerm = null,
                Type = null,
                Sorting = ProductSorting.DateCreated,
                CurrentPage = 1
            };

            return model;
        }

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

        private static Cart Cart()
        {
            Cart cart = new Cart()
            {
                User = new User
                {
                    Id = "TestId"
                }
            };

            return cart;
        }
    }
}
