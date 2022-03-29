namespace FitnessSite.Test.Routing
{
    using FitnessSite.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CartsControllerTest
    {
        [Fact]
        public void MyCartShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Carts/MyCart")
                .To<CartsController>(c => c.MyCart());

        [Fact]
        public void RemoveShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Carts/Remove")
                .To<CartsController>(c => c.Remove(With.Any<int>()));

        [Fact]
        public void BuyShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Carts/Buy")
                .To<CartsController>(c => c.Buy());

        [Fact]
        public void ClearShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Carts/Clear")
                .To<CartsController>(c => c.Clear());
    }
}
