﻿namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Products;
    using FitnessSite.Services.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
            var products = service.AllProducts(query);

            var totalProducts = service.TotalProducts();

            var productTypes = service.AllTypes();

            var productForm = this.mapper.Map<AllProductsQueryModel>(query);

            productForm.Products = products;
            productForm.TotalProducts = totalProducts;
            productForm.Types = productTypes;

            return this.View(productForm);
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

            return this.RedirectToAction("Details", new { id, information = product.ProductInformation() });
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
