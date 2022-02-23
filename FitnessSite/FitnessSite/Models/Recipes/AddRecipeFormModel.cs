namespace FitnessSite.Models.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddRecipeFormModel
    {
        [Required]
        [MinLength(RecipeTitleMinLength)]
        [MaxLength(RecipeTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(RecipeDescriptionMinLength)]
        public string Description { get; set; }
    }
}
