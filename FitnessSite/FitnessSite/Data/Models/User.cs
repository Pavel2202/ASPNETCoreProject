namespace FitnessSite.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {
        public User()
        {
            this.Cart = new Cart();
        }

        public int CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
