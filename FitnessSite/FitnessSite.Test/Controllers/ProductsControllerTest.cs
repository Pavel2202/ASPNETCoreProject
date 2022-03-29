﻿namespace FitnessSite.Test.Controllers
{
    using FitnessSite.Controllers;
    using FitnessSite.Data.Models.Enums;
    using FitnessSite.Models.Products;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;
    using static Areas.Admin.AdminConstants;
    using static Data.Products;
    using static WebConstants;

    public class ProductsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(TenPublicProducts))
                .Calling(c => c.All(GetQuery))
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
                    .WithSet<FitnessSite.Data.Models.Product>(products => products
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

        [Theory]
        [InlineData("pr",
            100.00,
            "https://www.silabg.com/uf/product/2945_pm_new.jpg",
            "Supplement",
            "Best protein. Buy only here.")]
        public void PostAddShouldBeForAdminsAndReturnViewWhenValidModelIsInvalid(
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
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductFormModel>());

        [Fact]
        public void DetailsShouldReturnView()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product))
                .Calling(c => c.Details(1, "Protein"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductDetailsViewModel>());

        [Fact]
        public void DetailsShouldReturnBadRequestWhenInformationIsInvalid()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product))
                .Calling(c => c.Details(1, "Creatine"))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void GetEditShouldBeForAdminsAndReturnView()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(1,
            "Protein",
            100.00,
            "https://www.silabg.com/uf/product/2945_pm_new.jpg",
            "Supplement",
            "Best protein. Buy only here.")]
        public void PostEditShouldBeForAdminsAndReturnRedirectWithValidModel(
            int id,
            string name,
            decimal price,
            string imageUrl,
            string type,
            string description)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product)
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

        [Theory]
        [InlineData(1,
            "Pr",
            100.00,
            "https://www.silabg.com/uf/product/2945_pm_new.jpg",
            "Supplement",
            "Best protein. Buy only here.")]
        public void PostEditShouldBeForAdminsAndReturnViewWhenModelIsInvalid(
            int id,
            string name,
            decimal price,
            string imageUrl,
            string type,
            string description)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product)
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
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductFormModel>());

        [Theory]
        [InlineData(2,
            "Protein",
            100.00,
            "https://www.silabg.com/uf/product/2945_pm_new.jpg",
            "Supplement",
            "Best protein. Buy only here.")]
        public void PostEditShouldBeForAdminsAndReturnBadRequestWhenIdIsInvalid(
            int id,
            string name,
            decimal price,
            string imageUrl,
            string type,
            string description)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product)
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
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldBeForAdminsAndReturnRedirect()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(Product)
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
                    .WithData(Product)
                    .WithData(Cart)
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

        
    }
}
