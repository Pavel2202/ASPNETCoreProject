namespace FitnessSite.Areas.Admin.Controllers
{
    using FitnessSite.Models.Recipes;
    using FitnessSite.Services.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : AdminController
    {
        private readonly IRecipeService service;

        public RecipesController(IRecipeService service)
        {
            this.service = service;
        }

        public IActionResult All(string currentPage)
        {
            var totalRecipes = service.TotalRecipesAdminArea();

            int page = 1;

            if (currentPage != null)
            {
                page = int.Parse(currentPage);
            }

            var recipes = new AllRecipesQueryModel
            {
                CurrentPage = page,
                TotalRecipes = totalRecipes
            };

            recipes.Recipes = service.All(null,
                RecipeSorting.DateCreated,
                recipes.CurrentPage,
                isPublic: false);

            return this.View(recipes);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.service.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
