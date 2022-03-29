namespace FitnessSite.Test.Admin.Controllers
{
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Trainers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Trainers;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<TrainersController>
                .Instance()
                .Calling(c => c.All("1"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllTrainersQueryModel>());

        [Fact]
        public void ChangeVisibilityShouldReturnRedirect()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();
    }
}
