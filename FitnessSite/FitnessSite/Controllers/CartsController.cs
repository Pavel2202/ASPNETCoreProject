namespace FitnessSite.Controllers
{
    using FitnessSite.Infrastructure.Extensions;
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
            var products = service.Products(this.User.Id());

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
            service.Buy(this.User.Id());

            return this.RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        public IActionResult Clear()
        {
            service.Clear(this.User.Id());

            return this.RedirectToAction("MyCart", "Carts");
        }
    }
}
