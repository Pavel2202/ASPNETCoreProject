namespace FitnessSite.Controllers
{
    using FitnessSite.Infrastructure;
    using FitnessSite.Models.Products;
    using FitnessSite.Services.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class ProductsController : Controller
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var products = service.AllProducts(query);

            var totalProducts = service.TotalProducts();

            var productTypes = service.AllTypes();

            return this.View(new AllProductsQueryModel
            {
                Products = products,
                SearchTerm = query.SearchTerm,
                Sorting = query.Sorting,
                TotalProducts = totalProducts,
                Types = productTypes
            });
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.User.IsAdmin())
            {
                return BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            service.CreateProduct(model);

            return this.RedirectToAction("All", "Products");
        }

        public IActionResult Details(int id, string information)
        {
            var product = service.GetProduct(id);

            if (information != product.ProductInformation())
            {
                return BadRequest();
            }

            return this.View(product);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.User.IsAdmin())
            {
                return BadRequest();
            }

            var product = service.GetProduct(id);

            var model = service.EditConvert(product);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ProductFormModel model)
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

            return this.RedirectToAction("Details", new { id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.User.IsAdmin())
            {
                return BadRequest();
            }

            service.Delete(id);

            return this.RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            service.AddToCart(id, this.User.Id());

            return this.RedirectToAction("All", "Products");
        }
    }
}
