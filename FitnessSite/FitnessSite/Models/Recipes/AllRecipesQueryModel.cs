namespace FitnessSite.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllRecipesQueryModel
    {
        public const int RecipesPerPage = 6;

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public RecipeSorting Sorting { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int TotalRecipes { get; set; }

        public IEnumerable<RecipeListingViewModel> Recipes { get; set; }
    }
}
