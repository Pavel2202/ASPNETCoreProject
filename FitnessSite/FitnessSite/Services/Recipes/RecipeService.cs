namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Recipes;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;

        public RecipeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateRecipe(AddRecipeFormModel model, string userId)
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
