namespace FitnessSite.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Sport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(SportNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(SportOriginMaxLength)]
        public string Origin { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
