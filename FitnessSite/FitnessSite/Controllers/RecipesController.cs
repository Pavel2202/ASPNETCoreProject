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

        public IActionResult All(string searchTerm)
        {
            var recipes = service.AllRecipes(searchTerm);

            return this.View(new AllRecipesQueryModel
            {
                Recipes = recipes,
                SearchTerm = searchTerm
            });
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
    }
}
