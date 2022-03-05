namespace FitnessSite.Services.Carts
{
    using FitnessSite.Models.Carts;
    using System.Collections.Generic;

    public interface ICartsService
    {
        IEnumerable<ProductsViewModel> Products(string userId);

        void Remove(int productId);

        void Buy(string userId);

        void Clear(string userId);
    }
}
