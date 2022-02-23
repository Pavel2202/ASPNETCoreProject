namespace FitnessSite.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {
        public int CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
