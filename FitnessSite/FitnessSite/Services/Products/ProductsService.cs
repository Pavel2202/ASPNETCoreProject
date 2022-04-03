﻿namespace FitnessSite.Services.Products
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Data.Models.Enums;
    using FitnessSite.Models.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly FitnessSiteDbContext context;
        private readonly IMapper mapper;

        public ProductsService(FitnessSiteDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddToCart(int productId, string userId)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            var product = context.Products
                .FirstOrDefault(p => p.Id == productId);

            var cartId = user.CartId;

            var cart = context.Carts
                .FirstOrDefault(c => c.Id == cartId);

            cart.Products.Add(product);

            product.CartId = cartId;

            context.SaveChanges();
        }

        public IEnumerable<ProductListingViewModel> All(
            string searchTerm = null,
            string type = null,
            ProductSorting sorting = ProductSorting.DateCreated,
            int currentPage = 1,
            int productsPerPage = int.MaxValue,
            bool isPublic = true)
        {
            var productsQuery = context.Products
                .Where(p => !isPublic || p.IsPublic)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
            {
                var typeEnum = Enum.Parse(typeof(ProductType), type);

                productsQuery = productsQuery.Where(p => p.Type == (ProductType)typeEnum);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.Title => productsQuery.OrderBy(p => p.Name),
                ProductSorting.Type => productsQuery.OrderBy(p => p.Type),
                ProductSorting.Price => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var products = productsQuery
                .Skip((currentPage - 1) * AllProductsQueryModel.ProductsPerPage)
                .Take(AllProductsQueryModel.ProductsPerPage)
                .ProjectTo<ProductListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return products;
        }

        public IEnumerable<string> AllTypes()
        {
            List<string> types = new List<string>();

            types.Add("Supplement");
            types.Add("Equipment");

            return types;
        }

        public void ChangeVisibility(int id)
        {
            var product = context.Products
                .FirstOrDefault(p => p.Id == id);

            var state = product.IsPublic;

            product.IsPublic = !state;

            context.SaveChanges();
        }

        public void CreateProduct(ProductFormModel model)
        {
            var type = Enum.Parse(typeof(ProductType), model.Type);

            var product = mapper.Map<Product>(model);

            product.Type = (ProductType)type;

            product.IsPublic = true;

            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = context.Products
                .Where(p => p.Id == id).FirstOrDefault();

            if (product is null)
            {
                return;
            }

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public bool Edit(int id, ProductFormModel model)
        {
            var product = context.Products
                .Where(p => p.Id == id).FirstOrDefault();

            if (product is null)
            {
                return false;
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.Description = model.Description;
            product.IsPublic = false;

            var type = Enum.Parse(typeof(ProductType), model.Type);

            product.Type = (ProductType)type;

            context.SaveChanges();

            return true;
        }

        public ProductFormModel EditConvert(ProductDetailsViewModel product)
        {
            var type = context.Products
                .Where(t => t.Name == product.Name)
                .Select(t => t.Type.ToString()).FirstOrDefault();

            var model = mapper.Map<ProductFormModel>(product);

            model.Type = type;

            return model;
        }

        public ProductDetailsViewModel GetProduct(int id)
            => context.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDetailsViewModel>(mapper.ConfigurationProvider)
                .First();

        public int TotalProducts()
            => context.Products.Where(p => p.IsPublic).Count();

        public int TotalProductsAdminArea()
            => context.Products.Count();
    }
}
