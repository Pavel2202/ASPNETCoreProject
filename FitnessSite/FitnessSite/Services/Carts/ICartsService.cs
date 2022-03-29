namespace FitnessSite.Services.Carts
{
    using FitnessSite.Models.Carts;
    using System.Collections.Generic;

    public interface ICartsService
    {
        IEnumerable<ProductViewModel> Products(string userId);

        void Remove(int productId);

        void Buy(string userId);

        void Clear(string userId);
    }
}
