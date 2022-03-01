namespace FitnessSite.Services.Products
{
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext context;

        public ProductsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductListingViewModel> AllProducts(AllProductsQueryModel query)
        {
            var productsQuery = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Type))
            {
                var type = Enum.Parse(typeof(ProductType), query.Type);

                productsQuery = productsQuery.Where(p => p.Type == (ProductType)type);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(r =>
                    r.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productsQuery = query.Sorting switch
            {
                ProductSorting.Title => productsQuery.OrderByDescending(p => p.Name),
                ProductSorting.Type => productsQuery.OrderByDescending(p => p.Type),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var products = productsQuery
                .Skip((query.CurrentPage - 1) * AllProductsQueryModel.ProductsPerPage)
                .Take(AllProductsQueryModel.ProductsPerPage)
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl                 
                }).ToList();

            return products;
        }

        public IEnumerable<string> AllTypes()
        {
            List<string> types = new List<string>();

            types.Add("Supplement");
            types.Add("Equipment");

            return types;
        }

        public void CreateProduct(ProductFormModel model)
        {
            var type = Enum.Parse(typeof(ProductType), model.Type);

            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Type = (ProductType)type,
                Description = model.Description
            };

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

            var model = new ProductFormModel
            {
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Type = type,
                Description = product.Description
            };

            return model;
        }

        public ProductDetailsViewModel GetProduct(int id)
            => context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description
                }).First();

        public int TotalProducts()
            => context.Products.Count();
    }
}
