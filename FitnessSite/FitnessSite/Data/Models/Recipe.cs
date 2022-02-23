namespace FitnessSite.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(RecipeTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public User Creator{ get; set; }
    }
}
