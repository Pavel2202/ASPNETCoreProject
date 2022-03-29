namespace FitnessSite.Test.Admin.Routing
{
    using FitnessSite.Areas.Admin.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SportsControllerTest
    {
            [Fact]
            public void AllShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Admin/Sports/All")
                    .To<SportsController>(c => c.All(With.Any<string>()));

            [Fact]
            public void ChangeVisibilityShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Admin/Sports/ChangeVisibility")
                    .To<SportsController>(c => c.ChangeVisibility(With.Any<int>()));
    }
}
