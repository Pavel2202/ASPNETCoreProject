namespace FitnessSite.Controllers
{
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipes = service.MyRecipes(userId);

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            service.CreateRecipe(recipe, userId);

            return this.RedirectToAction("All", "Recipes");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!service.IsCreatorOfRecipe(id, userId))
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!service.IsCreatorOfRecipe(id, userId))
            {
                return Unauthorized();
            }

            service.Delete(id);

            return this.RedirectToAction("Mine", "Recipes");
        }
    }
}
