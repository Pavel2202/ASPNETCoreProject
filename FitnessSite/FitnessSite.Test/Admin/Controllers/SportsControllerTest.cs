namespace FitnessSite.Test.Admin.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Sports;
    using FitnessSite.Data.Models;

    public class SportsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<SportsController>
                .Instance()
                .Calling(c => c.All("1"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllSportsQueryModel>());

        [Fact]
        public void ChangeVisibilityShouldReturnRedirect()
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport()))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();

        private static Sport Sport()
        {
            var sport = new Sport()
            {
                Id = 1,
                Name = "Football",
                Origin = "England",
                Description = "The most played sport in the world. It is also the most expensive sport.",
                IsPublic = true
            };

            return sport;
        }
    }
}
