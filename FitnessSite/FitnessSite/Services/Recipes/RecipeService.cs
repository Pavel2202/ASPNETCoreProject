namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;

        public RecipeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<RecipeListingViewModel> AllRecipes(string searchTerm)
        {
            var recipesQuery = context.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r => 
                    r.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            var recipes = recipesQuery
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl
                }).ToList();

            return recipes;
        }

        public void CreateRecipe(RecipeFormModel model, string userId)
        {
            Recipe recipe = new Recipe()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                CreatorId = userId
            };

            context.Recipes.Add(recipe);

            context.SaveChanges();
        }
    }
}
