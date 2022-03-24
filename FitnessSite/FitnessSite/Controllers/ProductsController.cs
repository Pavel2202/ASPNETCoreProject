namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Products;
    using FitnessSite.Services.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;
    using static Areas.Admin.AdminConstants;

    public class ProductsController : Controller
    {
        private readonly IProductsService service;
        private readonly IMapper mapper;

        public ProductsController(IProductsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var products = service.AllProducts(
                query.SearchTerm,
                query.Type,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage
                );

            var totalProducts = service.TotalProducts();

            var productTypes = service.AllTypes();

            var productForm = this.mapper.Map<AllProductsQueryModel>(query);

            productForm.Products = products;
            productForm.TotalProducts = totalProducts;
            productForm.Types = productTypes;

            return this.View(productForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            service.CreateProduct(model);

            TempData[GlobalMessageKey] = "You successfully added a product!";

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

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var product = service.GetProduct(id);

            var model = service.EditConvert(product);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            if (!ModelState.IsValid)
            {
                return this.View(product);
            }

            var edited = service.Edit(id, product);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "You successfully edited a product!";

            return this.RedirectToAction("Details", new { id, information = product.ProductInformation() });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            service.Delete(id);

            TempData[GlobalMessageKey] = "You successfully deleted a product!";

            return this.RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            service.AddToCart(id, this.User.Id());

            TempData[GlobalMessageKey] = "You successfully added a product to your cart!";

            return this.RedirectToAction("All", "Products");
        }
    }
}
