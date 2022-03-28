namespace FitnessSite.Test.Admin.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Areas.Admin.Controllers;
    using FitnessSite.Models.Trainers;
    using FitnessSite.Data.Models;

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
                    .WithData(Trainer()))
                .Calling(c => c.ChangeVisibility(1))
                .ShouldReturn()
                .Redirect();

        private static Trainer Trainer()
        {
            var trainer = new Trainer()
            {
                Id = 1,
                FullName = "Pavel Timenov",
                Email = "paveltimenov@abv.bg",
                PhoneNumber = "514752368",
                ImageUrl = "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
                Description = "Hello. I am Pavel Timenov. Taekwondo black belt.",
                SportId = 1,
                Sport = new Sport
                {
                    Name = "Football",
                    Origin = "England",
                    Description = "The best game. It is the most played in the world."
                },
                UserId = "TestId",
                IsPublic = true
            };

            return trainer;
        }
    }
}
