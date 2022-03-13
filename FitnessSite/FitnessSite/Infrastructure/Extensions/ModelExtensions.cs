namespace FitnessSite.Infrastructure.Extensions
{
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Sports;
    using FitnessSite.Models.Trainers;

    public static class ModelExtensions
    {
        public static string RecipeInformation(this IRecipeModel recipe)
            => recipe.Title;

        public static string ProductInformation(this IProductModel product)
            => product.Name;

        public static string SportInformation(this ISportsModel sport)
            => sport.Name + "-" + sport.Origin;

        public static string TrainerInformation(this ITrainerModel trainer)
            => trainer.FullName + "-" + trainer.Sport;
    }
}
