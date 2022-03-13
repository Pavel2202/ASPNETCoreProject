namespace FitnessSite.Infrastructure
{
    using AutoMapper;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Products;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductListingViewModel>();
            this.CreateMap<ProductDetailsViewModel, ProductFormModel>();
            this.CreateMap<Product, ProductDetailsViewModel>();
        }
    }
}
