namespace FitnessSite.Services.Home
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public HomeService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<TrainerListingViewModel> BestTrainers()
            => context.Trainers
                .OrderByDescending(t => t.Customers.Count)
                .Take(3)
                .ProjectTo<TrainerListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

        public ProductListingViewModel DailyProduct()
        {
            var allProducts = context.Products
                .ProjectTo<ProductListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            Random random = new Random();

            var productIndex = random.Next(0, allProducts.Count);;

            var product = allProducts[productIndex];

            return product;
        }

        public RecipeListingViewModel DailyRecipe()
        {
            var allRecipes = context.Recipes
                .ProjectTo<RecipeListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            Random random = new Random();

            var recipeIndex = random.Next(0, allRecipes.Count);

            var recipe = allRecipes[recipeIndex];

            return recipe;
        }
    }
}
