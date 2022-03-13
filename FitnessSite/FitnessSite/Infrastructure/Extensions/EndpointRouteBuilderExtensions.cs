namespace FitnessSite.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
           => endpoints.MapControllerRoute(
               name: "Areas",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        public static void MapRecipeRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                        name: "Recipe Details",
                        pattern: "/Recipes/Details/{id}/{information}",
                        defaults: new { controller = "Recipes", action = "Details" });

        public static void MapProductRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                        name: "Product Details",
                        pattern: "/Product/Details/{id}/{information}",
                        defaults: new { controller = "Products", action = "Details" });

        public static void MapSportRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                        name: "Sport Details",
                        pattern: "/Sports/Details/{id}/{information}",
                        defaults: new { controller = "Sports", action = "Details" });

        public static void MapTrainerRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                        name: "Trainer Details",
                        pattern: "/Trainers/Details/{id}/{information}",
                        defaults: new { controller = "Trainers", action = "Details" });
    }
}
