namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using FitnessSite.Data.Models.Enums;

    public class Carts
    {
        public static Product Product
            => new Product()
            {
                Id = 1,
                Name = "Protein",
                Price = 100,
                Type = ProductType.Supplement,
                ImageUrl = "https://www.silabg.com/uf/product/2945_pm_new.jpg",
                Description = "Best protein. Buy only here.",
                CartId = 1
            };

        public static Cart Cart
            => new Cart()
            {
                Id = 1,
                User = new User
                {
                    Id = "TestId"
                }
            };
    }
}
