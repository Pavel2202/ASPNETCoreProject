namespace FitnessSite.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cart
    {
        public Cart()
        {
            this.Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
