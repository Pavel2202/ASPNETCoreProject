namespace FitnessSite.Controllers
{
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
            => this.View();

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

        [Authorize]
        public IActionResult Details(int id)
        {
            var product = service.GetProduct(id);

            return this.View(product);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
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

            var product = service.GetProduct(id);

            return this.RedirectToAction("Details", new { id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            service.Delete(id);

            return this.RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            service.AddToCart(id, userId);

            return this.RedirectToAction("All", "Products");
        }
    }
}
