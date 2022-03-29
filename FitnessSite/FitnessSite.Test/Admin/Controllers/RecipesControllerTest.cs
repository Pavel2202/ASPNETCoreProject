namespace FitnessSite.Test.Admin.Controllers
{
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Recipes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Recipes;

    public class RecipesControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<RecipesController>
                .Instance()
                .Calling(c => c.All("1"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllRecipesQueryModel>());

        [Fact]
        public void ChangeVisibilityShouldReturnRedirect()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();
    }
}
