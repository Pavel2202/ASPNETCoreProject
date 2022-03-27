namespace FitnessSite.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Trainers;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/All")
                .To<TrainersController>(c => c.All(With.Any<AllTrainersQueryModel>()));

        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/Become")
                .To<TrainersController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Trainers/Become")
                    .WithMethod(HttpMethod.Post))
                .To<TrainersController>(c => c.Become(With.Any<BecomeTrainerFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/Details")
                .To<TrainersController>(c => c.Details(With.Any<int>(), With.Any<string>()));

        [Fact]
        public void GetEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/Edit")
                .To<TrainersController>(c => c.Edit(With.Any<int>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Trainers/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<TrainersController>(c => c.Edit(With.Any<int>(), With.Any<BecomeTrainerFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/Delete")
                .To<TrainersController>(c => c.Delete(With.Any<int>()));

        [Fact]
        public void MyProfileShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/MyProfile")
                .To<TrainersController>(c => c.MyProfile(With.Any<string>()));

        [Fact]
        public void HireShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/Hire")
                .To<TrainersController>(c => c.Hire(With.Any<int>()));
    }
}
