namespace FitnessSite.Models.Recipes
{
    public class RecipeListingViewModel : IRecipeModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
