namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Models.Recipes;
    using System.Collections.Generic;

    public interface IRecipeService
    {
        IEnumerable<RecipeListingViewModel> AllRecipes(AllRecipesQueryModel query);

        void CreateRecipe(RecipeFormModel recipe, string userId);

        int TotalRecipes();
    }
}
