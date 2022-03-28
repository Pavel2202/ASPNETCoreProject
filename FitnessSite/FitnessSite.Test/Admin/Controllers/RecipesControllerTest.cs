namespace FitnessSite.Test.Admin.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Data.Models;

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
                    .WithData(Recipe()))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();

        private static Recipe Recipe()
        {
            var recipe = new Recipe()
            {
                Id = 1,
                Title = "Scrambled eggs",
                ImageUrl = "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
                Description = "Crack the eggs. Put then in the pan and mix.",
                Creator = new User()
                {
                    Id = "TestId",
                    UserName = "TestUser"
                },
                IsPublic = true
            };

            return recipe;
        }
    }
}
