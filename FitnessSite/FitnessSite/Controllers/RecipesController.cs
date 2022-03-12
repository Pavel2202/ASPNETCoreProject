namespace FitnessSite.Controllers
{
    using FitnessSite.Infrastructure;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Services.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class RecipesController : Controller
    {
        private readonly IRecipeService service;

        public RecipesController(IRecipeService service)
        {
            this.service = service;
        }

        public IActionResult All([FromQuery] AllRecipesQueryModel query)
        {
            var recipes = service.AllRecipes(query);

            var totalRecipes = service.TotalRecipes();

            return this.View(new AllRecipesQueryModel
            {
                Recipes = recipes,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalRecipes = totalRecipes,
                Sorting = query.Sorting
            });
        }

        public IActionResult Details(int id)
        {
            var recipe = service.GetRecipe(id);

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

            return this.RedirectToAction("All", "Recipes");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!service.IsCreatorOfRecipe(id, this.User.Id()))
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

            return RedirectToAction("Details", new { id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!service.IsCreatorOfRecipe(id, this.User.Id()))
            {
                return Unauthorized();
            }

            service.Delete(id);

            return this.RedirectToAction("Mine", "Recipes");
        }
    }
}
