namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Models.Recipes;
    using System.Collections.Generic;

    public interface IRecipeService
    {
        IEnumerable<RecipeListingViewModel> AllRecipes(string searchTerm);

        void CreateRecipe(RecipeFormModel recipe, string userId);
    }
}
