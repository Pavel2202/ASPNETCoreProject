namespace FitnessSite.Services.Home
{
    using FitnessSite.Data;
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Trainers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext context;

        public HomeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TrainerListingViewModel> BestTrainers()
            => context.Trainers
                .OrderByDescending(t => t.Customers.Count)
                .Select(t => new TrainerListingViewModel
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Sport = t.Sport.Name,
                    ImageUrl = t.ImageUrl
                }).Take(3)
            .ToList();

        public ProductListingViewModel DailyProduct()
        {
            var allProducts = context.Products
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .ToList();

            Random random = new Random();

            var productIndex = random.Next(0, allProducts.Count);;

            var product = allProducts[productIndex];

            return product;
        }

        public RecipeListingViewModel DailyRecipe()
        {
            var allRecipes = context.Recipes
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl
                })
                .ToList();

            Random random = new Random();

            var recipeIndex = random.Next(0, allRecipes.Count);

            var recipe = allRecipes[recipeIndex];

            return recipe;
        }
    }
}
