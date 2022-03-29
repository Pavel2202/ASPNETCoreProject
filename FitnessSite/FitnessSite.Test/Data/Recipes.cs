namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public static class Recipes
    {
        public static IEnumerable<Recipe> TenPublicRecipes
            => Enumerable.Range(0, 10).Select(p => new Recipe
            {
                IsPublic = true
            });

        public static AllRecipesQueryModel GetQuery
            => new AllRecipesQueryModel
            {
                SearchTerm = null,
                Sorting = RecipeSorting.DateCreated,
                CurrentPage = 1
            };

        public static Recipe Recipe
            => new Recipe()
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

        public static Recipe SecondRecipe
            => new Recipe()
            {
                Id = 2,
                Title = "Rice",
                ImageUrl = "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
                Description = "Crack the eggs. Put then in the pan and mix.",
                Creator = new User()
                {
                    Id = "InvalidId",
                    UserName = "InvalidUserName"
                },
                IsPublic = true
            };
    }
}
