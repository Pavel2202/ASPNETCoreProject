namespace FitnessSite.Models.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddRecipeFormModel
    {
        [Required]
        [StringLength(RecipeTitleMaxLength, MinimumLength = RecipeTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = RecipeDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }
    }
}
