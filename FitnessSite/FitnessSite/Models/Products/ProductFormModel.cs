namespace FitnessSite.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ProductFormModel
    {
        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
        public string Name { get; set; }

        [Required]
        [Range(ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = ProductDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }
    }
}
