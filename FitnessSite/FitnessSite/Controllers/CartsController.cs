namespace FitnessSite.Controllers
{
    using FitnessSite.Services.Carts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class CartsController : Controller
    {
        private readonly ICartsService service;

        public CartsController(ICartsService service)
        {
            this.service = service;
        }

        [Authorize]
        public IActionResult MyCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var products = service.Products(userId);

            return this.View(products);
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            service.Remove(id);

            return this.RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        public IActionResult Buy()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            service.Buy(userId);

            return this.RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        public IActionResult Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            service.Clear(userId);

            return this.RedirectToAction("MyCart", "Carts");
        }
    }
}
