namespace FitnessSite.Services.Carts
{
    using FitnessSite.Data;
    using FitnessSite.Models.Carts;
    using System.Collections.Generic;
    using System.Linq;

    public class CartsService : ICartsService
    {
        private readonly ApplicationDbContext context;

        public CartsService(ApplicationDbContext context)
        {
            this.context = context;
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
                .Select(p => new ProductsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                }).ToList();

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
