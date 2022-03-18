namespace FitnessSite.Models.Home
{
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Trainers;
    using System.Collections.Generic;

    public class IndexDisplayViewModel
    {
        public RecipeListingViewModel Recipe { get; set; }

        public ProductListingViewModel Product { get; set; }

        public TrainerListingViewModel Trainer { get; set; }
    }
}
