namespace FitnessSite.Test.Admin.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;

    public class RecipesControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Recipes/All")
                .To<RecipesController>(c => c.All(With.Any<string>()));

        [Fact]
        public void ChangeVisibilityShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Recipes/ChangeVisibility")
                .To<RecipesController>(c => c.ChangeVisibility(With.Any<int>()));
    }
}
