namespace FitnessSite.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Recipes;

    public class RecipesControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Recipes/All")
                .To<RecipesController>(c => c.All(With.Any<AllRecipesQueryModel>()));

        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Recipes/Add")
                .To<RecipesController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Recipes/Add")
                    .WithMethod(HttpMethod.Post))
                .To<RecipesController>(c => c.Add(With.Any<RecipeFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Recipes/Details")
                .To<RecipesController>(c => c.Details(With.Any<int>(), With.Any<string>()));

        [Fact]
        public void GetEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Recipes/Edit")
                .To<RecipesController>(c => c.Edit(With.Any<int>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Recipes/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<RecipesController>(c => c.Edit(With.Any<int>(), With.Any<RecipeFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Recipes/Delete")
                .To<RecipesController>(c => c.Delete(With.Any<int>()));

        [Fact]
        public void MineShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Recipes/Mine")
                .To<RecipesController>(c => c.Mine());
    }
}
