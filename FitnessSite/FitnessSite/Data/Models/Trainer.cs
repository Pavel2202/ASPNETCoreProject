﻿namespace FitnessSite.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Trainer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TrainerFullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(TrainerEmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(TrainerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public int SportId { get; set; }

        [Required]
        public Sport Sport { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
