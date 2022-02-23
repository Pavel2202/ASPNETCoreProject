namespace FitnessSite.Data.Models
{
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

        public ProductType Type { get; set; }

        [Required]
        public string Description { get; set; }

        public int? CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
