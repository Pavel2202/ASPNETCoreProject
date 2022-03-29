namespace FitnessSite.Test.Admin.Controllers
{
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Sports;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Sports;

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
                    .WithData(Sport))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();
    }
}
