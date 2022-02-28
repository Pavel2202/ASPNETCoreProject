namespace FitnessSite.Services.Products
{
    using FitnessSite.Models.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        void CreateProduct(ProductFormModel model);

        IEnumerable<ProductListingViewModel> AllProducts(AllProductsQueryModel query);

        int TotalProducts();

        IEnumerable<string> AllTypes();
    }
}
