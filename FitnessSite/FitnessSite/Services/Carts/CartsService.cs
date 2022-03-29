namespace FitnessSite.Services.Carts
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Models.Carts;
    using System.Collections.Generic;
    using System.Linq;

    public class CartsService : ICartsService
    {
        private readonly FitnessSiteDbContext context;
        private readonly IMapper mapper;

        public CartsService(FitnessSiteDbContext context, IMapper mapper)
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

        public IEnumerable<ProductViewModel> Products(string userId)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            var cartId = user.CartId;

            var products = context.Products
                .Where(p => p.CartId == cartId)
                .ProjectTo<ProductViewModel>(mapper.ConfigurationProvider)
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
