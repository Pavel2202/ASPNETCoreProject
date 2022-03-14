﻿namespace FitnessSite.Services.Carts
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Models.Carts;
    using System.Collections.Generic;
    using System.Linq;

    public class CartsService : ICartsService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CartsService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Buy(string userId)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            var cartId = user.CartId;

            var products = context.Products
                .Where(p => p.CartId == cartId).ToList();

            foreach (var product in products)
            {
                product.CartId = null;
            }

            context.SaveChanges();
        }

        public void Clear(string userId)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            var cartId = user.CartId;

            var products = context.Products
                .Where(p => p.CartId == cartId).ToList();

            foreach (var product in products)
            {
                product.CartId = null;
            }

            context.SaveChanges();
        }

        public IEnumerable<ProductsViewModel> Products(string userId)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            var cartId = user.CartId;

            var products = context.Products
                .Where(p => p.CartId == cartId)
                .ProjectTo<ProductsViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return products;
        }

        public void Remove(int productId)
        {
            var product = context.Products
                .FirstOrDefault(p => p.Id == productId);

            product.CartId = null;

            context.SaveChanges();
        }
    }
}
