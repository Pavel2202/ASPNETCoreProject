namespace FitnessSite.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using System;
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Home;
    using FitnessSite.Models.Recipes;

    using static Data.Products;
    using static Data.Recipes;
    using static Data.Trainers;

    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldStoreProductCacheCorrectly()
            => MyController<HomeController>
            .Instance(controller => controller
                .WithData(TenPublicProducts)
                .WithData(TenPublicRecipes)
                .WithData(TenPublicTrainers))
            .Calling(c => c.Index())
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntry(entry => entry
                    .WithKey(DailyProductCacheKey)
                    .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromDays(1))
                    .WithValueOfType<ProductListingViewModel>()));

        [Fact]
        public void IndexShouldStoreRecipeCacheCorrectly()
            => MyController<HomeController>
            .Instance(controller => controller
                .WithData(TenPublicProducts)
                .WithData(TenPublicRecipes)
                .WithData(TenPublicTrainers))
            .Calling(c => c.Index())
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntry(entry => entry
                    .WithKey(DailyRecipeCacheKey)
                    .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromDays(1))
                    .WithValueOfType<RecipeListingViewModel>()));

        [Fact]
        public void IndexShouldReturnView()
            => MyController<HomeController>
            .Instance(controller => controller
                .WithData(TenPublicProducts)
                .WithData(TenPublicRecipes)
                .WithData(TenPublicTrainers))
            .Calling(c => c.Index())
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<IndexDisplayViewModel>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
