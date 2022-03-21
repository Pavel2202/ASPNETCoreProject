namespace FitnessSite.Areas.Admin.Controllers
{
    using FitnessSite.Models.Products;
    using FitnessSite.Services.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdminController
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        public IActionResult All(string currentPage)
        {
            var totalProducts = service.TotalProducts();

            int page = 1;

            if (currentPage != null)
            {
                page = int.Parse(currentPage);
            }

            var products = new AllProductsQueryModel
            {
                CurrentPage = page,
                TotalProducts = totalProducts
            };

            products.Products = service.AllProducts(
                null,
                null,
                ProductSorting.DateCreated,
                products.CurrentPage,
                isPublic: false
                );

            return this.View(products);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.service.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
