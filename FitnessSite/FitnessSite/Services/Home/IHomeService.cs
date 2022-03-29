namespace FitnessSite.Services.Home
{
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Trainers;

    public interface IHomeService
    {
        TrainerListingViewModel BestTrainer();

        RecipeListingViewModel DailyRecipe();

        ProductListingViewModel DailyProduct();
    }
}
