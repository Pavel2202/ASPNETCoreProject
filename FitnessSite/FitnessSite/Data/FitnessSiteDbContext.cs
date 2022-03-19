namespace FitnessSite.Data
{
    using FitnessSite.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FitnessSiteDbContext : IdentityDbContext<User>
    {
        public FitnessSiteDbContext(DbContextOptions<FitnessSiteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
