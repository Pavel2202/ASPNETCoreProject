﻿namespace FitnessSite.Services.Recipes
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Recipes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RecipeService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
                .ProjectTo<RecipeListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return recipes;
        }

        public void CreateRecipe(RecipeFormModel model, string userId)
        {
            var recipe = mapper.Map<Recipe>(model);

            recipe.CreatorId = userId;           

            context.Recipes.Add(recipe);

            context.SaveChanges();
        }

        public void Delete(int recipeId)
        {
            var recipe = context.Recipes
                .FirstOrDefault(r => r.Id == recipeId);

            if (recipe is null)
            {
                return;
            }

            context.Recipes.Remove(recipe);
            context.SaveChanges();
        }

        public bool Edit(int id, RecipeFormModel model)
        {
            var recipe = context.Recipes
                .FirstOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return false;
            }

            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.ImageUrl = model.ImageUrl;

            context.SaveChanges();

            return true;
        }

        public RecipeFormModel EditConvert(RecipeDetailsViewModel recipe)
        {
            var model = mapper.Map<RecipeFormModel>(recipe);

            return model;
        }

        public RecipeDetailsViewModel GetRecipe(int recipeId)
        {
            var recipe = context.Recipes
                .FirstOrDefault(r => r.Id == recipeId);

            var creator = context.Users.FirstOrDefault(u => u.Id == recipe.CreatorId);
            var username = creator.UserName.Split("@").ToArray().First();

            var result = mapper.Map<RecipeDetailsViewModel>(recipe);

            result.Creator = username;            

            return result;
        }

        public bool IsCreatorOfRecipe(int recipeId, string userId)
        {
            var recipe = context.Recipes
                .FirstOrDefault(r => r.Id == recipeId);

            if (recipe.CreatorId == userId)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<RecipeListingViewModel> MyRecipes(string userId)
            => context.Recipes
                .Where(r => r.CreatorId == userId)
                .ProjectTo<RecipeListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

        public int TotalRecipes()
            => context.Recipes.Count();
    }
}
