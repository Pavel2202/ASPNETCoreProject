namespace FitnessSite.Models.Sports
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessSite.Data.DataConstants;

    public class SportsFormModel : ISportsModel
    {
        [Required]
        [StringLength(SportNameMaxLength, MinimumLength = SportNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(SportOriginMaxLength, MinimumLength = SportOriginMinLength)]
        public string Origin { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = SportDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }
    }
}
