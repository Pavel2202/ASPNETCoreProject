namespace FitnessSite.Areas.Admin.Controllers
{
    using FitnessSite.Services.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdminController
    {
        private readonly IRecipeService service;

        public ProductsController(IRecipeService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}
