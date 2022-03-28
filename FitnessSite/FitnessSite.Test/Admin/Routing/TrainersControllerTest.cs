namespace FitnessSite.Test.Admin.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Admin/Trainers/All")
                    .To<TrainersController>(c => c.All(With.Any<string>()));

        [Fact]
        public void ChangeVisibilityShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Trainers/ChangeVisibility")
                .To<TrainersController>(c => c.ChangeVisibility(With.Any<int>()));
    }
}
