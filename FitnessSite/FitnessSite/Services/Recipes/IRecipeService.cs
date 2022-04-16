namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Models.Recipes;
    using System.Collections.Generic;

    public interface IRecipeService
    {
        IEnumerable<RecipeListingViewModel> All(
            string searchTerm = null,
            RecipeSorting sorting = RecipeSorting.DateCreated,
            int currentPage = 1,
            int recipesPerPage = int.MaxValue,
            bool isPublic = true);

        void CreateRecipe(RecipeFormModel recipe, string userId);

        int PublicRecipes();

        int TotalRecipes();

        int TotalRecipesAdminArea();

        RecipeDetailsViewModel GetRecipe(int recipeId);

        IEnumerable<RecipeListingViewModel> MyRecipes(string userId);

        bool IsCreatorOfRecipe(int recipeId, string userId);

        bool Edit(int id, RecipeFormModel model);

        RecipeFormModel ConvertToFormModel(RecipeDetailsViewModel recipe);

        void Delete(int recipeId);

        void ChangeVisibility(int id);
    }
}
