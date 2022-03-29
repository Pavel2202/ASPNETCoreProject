﻿namespace FitnessSite.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Cart = new Cart();
        }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public int? TrainerId { get; set; }

        public Trainer Trainer { get; set; }
    }
}
