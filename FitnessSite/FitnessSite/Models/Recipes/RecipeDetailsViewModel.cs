namespace FitnessSite.Models.Recipes
{
    public class RecipeDetailsViewModel : IRecipeModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Creator { get; set; }
    }
}
