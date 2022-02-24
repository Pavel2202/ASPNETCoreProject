namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Models.Recipes;

    public interface IRecipeService
    {
        void CreateRecipe(AddRecipeFormModel recipe, string userId);
    }
}
