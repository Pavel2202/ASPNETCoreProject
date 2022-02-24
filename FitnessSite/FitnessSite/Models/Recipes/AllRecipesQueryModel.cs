namespace FitnessSite.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllRecipesQueryModel
    {
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public RecipeSorting Sorting { get; set; }

        public IEnumerable<RecipeListingViewModel> Recipes { get; set; }
    }
}
