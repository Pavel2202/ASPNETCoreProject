namespace FitnessSite.Infrastructure
{
    using AutoMapper;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Carts;
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Sports;
    using FitnessSite.Models.Trainers;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductsViewModel>();
            this.CreateMap<Product, ProductListingViewModel>();
            this.CreateMap<ProductDetailsViewModel, ProductFormModel>();
            this.CreateMap<Product, ProductDetailsViewModel>();
            this.CreateMap<ProductFormModel, Product>();

            this.CreateMap<Recipe, RecipeListingViewModel>();
            this.CreateMap<Recipe, RecipeDetailsViewModel>();
            this.CreateMap<RecipeFormModel, Recipe>();
            this.CreateMap<RecipeDetailsViewModel, RecipeFormModel>();

            this.CreateMap<Sport, SportsListingViewModel>();
            this.CreateMap<Sport, SportsDetailsViewModel>();
            this.CreateMap<SportsFormModel, Sport>();
            this.CreateMap<SportsDetailsViewModel, SportsFormModel>();

            this.CreateMap<Sport, TrainerSportsViewModel>();
            this.CreateMap<Trainer, TrainerDetailsViewModel>()
                .ForMember(t => t.Sport, cfg => cfg.MapFrom(t => t.Sport.Name));
            this.CreateMap<BecomeTrainerFormModel, Trainer>()
                .ReverseMap();
            this.CreateMap<Trainer, TrainerListingViewModel>()
                .ForMember(t => t.Sport, cfg => cfg.MapFrom(t => t.Sport.Name));
        }
    }
}
