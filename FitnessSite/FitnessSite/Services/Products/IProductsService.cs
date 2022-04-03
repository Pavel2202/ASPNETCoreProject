namespace FitnessSite.Services.Products
{
    using FitnessSite.Models.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        void CreateProduct(ProductFormModel model);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null,
            string type = null,
            ProductSorting sorting = ProductSorting.DateCreated,
            int currentPage = 1,
            int productsPerPage = int.MaxValue,
            bool isPublic = true);

        int TotalProducts();

        int TotalProductsAdminArea();

        IEnumerable<string> AllTypes();

        ProductDetailsViewModel GetProduct(int id);

        ProductFormModel EditConvert(ProductDetailsViewModel product);

        bool Edit(int id, ProductFormModel model);

        void Delete(int id);

        void AddToCart(int productId, string userId);

        void ChangeVisibility(int id);
    }
}
