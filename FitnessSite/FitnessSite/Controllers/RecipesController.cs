namespace FitnessSite.Controllers
{
    using FitnessSite.Models.Recipes;
    using FitnessSite.Services.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly IRecipeService service;

        public RecipesController(IRecipeService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            return View();
        }
    }
}
