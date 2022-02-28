namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Models.Recipes;
    using System.Collections.Generic;

    public interface IRecipeService
    {
        IEnumerable<RecipeListingViewModel> AllRecipes(AllRecipesQueryModel query);

        void CreateRecipe(RecipeFormModel recipe, string userId);

        int TotalRecipes();

        DetailsViewModel GetRecipe(int recipeId);

        IEnumerable<MyRecipesViewModel> MyRecipes(string userId);

        bool IsCreatorOfRecipe(int recipeId, string userId);

        bool Edit(int id, RecipeFormModel model);

        RecipeFormModel EditConvert(DetailsViewModel recipe);

        void Delete(int recipeId);
    }
}
