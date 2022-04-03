namespace FitnessSite.Services.Home
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Trainers;
    using System;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly FitnessSiteDbContext context;
        private readonly IMapper mapper;

        public HomeService(FitnessSiteDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TrainerListingViewModel BestTrainer()
            => context.Trainers
                .Where(t => t.IsPublic)
                .OrderByDescending(t => t.Customers.Count)
                .ProjectTo<TrainerListingViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefault();

        public ProductListingViewModel DailyProduct()
        {
            var allProducts = context.Products
                .Where(p => p.IsPublic)
                .ProjectTo<ProductListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            Random random = new Random();

            var productIndex = random.Next(0, allProducts.Count);

            if (allProducts.Count == 0)
            {
                return null;
            }

            var product = allProducts[productIndex];

            return product;
        }

        public RecipeListingViewModel DailyRecipe()
        {
            var allRecipes = context.Recipes
                .Where(r => r.IsPublic)
                .ProjectTo<RecipeListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            Random random = new Random();

            var recipeIndex = random.Next(0, allRecipes.Count);

            if (allRecipes.Count == 0)
            {
                return null;
            }

            var recipe = allRecipes[recipeIndex];

            return recipe;
        }
    }
}
