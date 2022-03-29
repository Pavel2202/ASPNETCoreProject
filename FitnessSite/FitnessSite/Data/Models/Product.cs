namespace FitnessSite.Data.Models
{
    using FitnessSite.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public ProductType Type { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public int? CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
