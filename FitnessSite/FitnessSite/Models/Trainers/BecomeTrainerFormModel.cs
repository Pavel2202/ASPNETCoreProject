namespace FitnessSite.Models.Trainers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static FitnessSite.Data.DataConstants;

    public class BecomeTrainerFormModel
    {
        [Required]
        [StringLength(TrainerFullNameMaxLength, MinimumLength = TrainerFullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(TrainerEmailMaxLength, MinimumLength = TrainerEmailMinLength)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(TrainerPhoneNumberMaxLength, MinimumLength = TrainerPhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = TrainerDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }

        [Required]
        public int SportId { get; set; }

        public IEnumerable<TrainerSportsViewModel> Sports { get; set; }
    }
}
