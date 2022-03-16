﻿namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Services.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class RecipesController : Controller
    {
        private readonly IRecipeService service;
        private readonly IMapper mapper;

        public RecipesController(IRecipeService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllRecipesQueryModel query)
        {
            var recipes = service.AllRecipes(query);

            var totalRecipes = service.TotalRecipes();

            var recipeForm = this.mapper.Map<AllRecipesQueryModel>(query);

            recipeForm.Recipes = recipes;

            return this.View(recipeForm);
        }

        public IActionResult Details(int id, string information)
        {
            var recipe = service.GetRecipe(id);

            if (information != recipe.RecipeInformation())
            {
                return BadRequest();
            }

            return this.View(recipe);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var recipes = service.MyRecipes(this.User.Id());

            return this.View(recipes);
        }

        [Authorize]
        public IActionResult Add()
            => this.View();

        [HttpPost]
        [Authorize]
        public IActionResult Add(RecipeFormModel recipe)
        {
            if (!ModelState.IsValid)
            {
                return this.View(recipe);
            }

            service.CreateRecipe(recipe, this.User.Id());

            TempData[GlobalMessageKey] = "You successfully added a recipe!";

            return this.RedirectToAction("All", "Recipes");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!service.IsCreatorOfRecipe(id, this.User.Id()) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var recipe = service.GetRecipe(id);

            var model = service.EditConvert(recipe);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, RecipeFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var edited = service.Edit(id, model);

            if (!edited)
            {
                return BadRequest();
            }

            var recipe = service.GetRecipe(id);

            TempData[GlobalMessageKey] = "You successfully edited a recipe!";

            return RedirectToAction("Details", new { id, information = recipe.RecipeInformation() });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!service.IsCreatorOfRecipe(id, this.User.Id()) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            service.Delete(id);

            TempData[GlobalMessageKey] = "You successfully deleted a recipe!";

            return this.RedirectToAction("Mine", "Recipes");
        }
    }
}
