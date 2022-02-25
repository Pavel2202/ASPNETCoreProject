﻿namespace FitnessSite.Services.Recipes
{
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Recipes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;

        public RecipeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<RecipeListingViewModel> AllRecipes(AllRecipesQueryModel query)
        {
            var recipesQuery = context.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Title.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            recipesQuery = query.Sorting switch
            {
                RecipeSorting.Title => recipesQuery.OrderByDescending(r => r.Title),
                RecipeSorting.DateCreated or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var recipes = recipesQuery
                .Skip((query.CurrentPage - 1) * AllRecipesQueryModel.RecipesPerPage)
                .Take(AllRecipesQueryModel.RecipesPerPage)
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl
                }).ToList();

            return recipes;
        }

        public void CreateRecipe(RecipeFormModel model, string userId)
        {
            Recipe recipe = new Recipe()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                CreatorId = userId
            };

            context.Recipes.Add(recipe);

            context.SaveChanges();
        }

        public DetailsViewModel GetRecipe(int recipeId)
        {
            var recipe = context.Recipes
                .FirstOrDefault(r => r.Id == recipeId);

            var creator = context.Users.FirstOrDefault(u => u.Id == recipe.CreatorId);
            var username = creator.UserName.Split("@").ToArray().First();

            var result = new DetailsViewModel
            {
                Title = recipe.Title,
                ImageUrl = recipe.ImageUrl,
                Description = recipe.Description,
                Creator = username
            };

            return result;
        }

        public IEnumerable<MyRecipesViewModel> MyRecipes(string userId)
            => context.Recipes
                .Where(r => r.CreatorId == userId)
                .Select(r => new MyRecipesViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl
                }).ToList();

        public int TotalRecipes()
            => context.Recipes.Count();
    }
}
