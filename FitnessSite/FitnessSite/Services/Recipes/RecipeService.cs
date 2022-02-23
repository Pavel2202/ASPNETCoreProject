namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Data;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;

        public RecipeService(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
