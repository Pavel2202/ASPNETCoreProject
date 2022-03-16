namespace FitnessSite.Controllers
{
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Services.Carts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

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

            TempData[GlobalMessageKey] = "You successfully removed an item!";

            return this.RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        public IActionResult Buy()
        {
            service.Buy(this.User.Id());

            TempData[GlobalMessageKey] = "Your order was successful!";

            return this.RedirectToAction("MyCart", "Carts");
        }

        [Authorize]
        public IActionResult Clear()
        {
            service.Clear(this.User.Id());

            TempData[GlobalMessageKey] = "You successfully cleared your cart!";

            return this.RedirectToAction("MyCart", "Carts");
        }
    }
}
