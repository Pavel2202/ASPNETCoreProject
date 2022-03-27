namespace FitnessSite.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Trainers;
    using FitnessSite.Data.Models;
    using System.Linq;

    using static Data.Trainers;
    using static Data.Sports;

    using static WebConstants;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(TenPublicTrainers))
                .Calling(c => c.All(GetQuery()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllTrainersQueryModel>());

        [Fact]
        public void GetBecomeShouldBeAuthorizedAndReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<BecomeTrainerFormModel>());

        [Theory]
        [InlineData("Pavel Timenov",
            "paveltimenov@abv.bg",
            "514752368",
            "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
            "Hello. I am Pavel Timenov. Taekwondo black belt.",
            1)]
        public void PostBecomeShouldBeAuthorizedAndReturnRedirectWithValidModel(
            string fullName,
            string email,
            string phoneNumber,
            string imageUrl,
            string description,
            int sportId)
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(TenPublicSports)
                    .WithData(User())
                    .WithUser())
                .Calling(c => c.Become(new BecomeTrainerFormModel
                {
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    ImageUrl = imageUrl,
                    Description = description,
                    SportId = sportId
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Trainer>(trainers => trainers
                        .Any(t =>
                            t.FullName == fullName &&
                            t.Email == email &&
                            t.PhoneNumber == phoneNumber &&
                            t.ImageUrl == imageUrl &&
                            t.Description == description &&
                            t.SportId == sportId)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TrainersController>(c => c.All(With.Any<AllTrainersQueryModel>())));


        [Fact]
        public void DetailsShouldReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer()))
                .Calling(c => c.Details(1, $"Pavel Timenov-{Sport()}"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<TrainerDetailsViewModel>());

        [Fact]
        public void GetEditShouldBeAuthorizedAndReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer())
                    .WithData(TenPublicSports)
                    .WithData(User())
                    .WithUser())
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(1,
            "Pavel Timenov",
            "paveltimenov@abv.bg",
            "514752368",
            "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
            "Hello. I am Pavel Timenov. Taekwondo black belt.",
            1)]
        public void PostEditShouldBeAuthorizedAndReturnRedirectWithValidModel(
            int id,
            string fullName,
            string email,
            string phoneNumber,
            string imageUrl,
            string description,
            int sportId)
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer())
                    .WithData(TenPublicSports)
                    .WithData(User())
                    .WithUser())
                .Calling(c => c.Edit(id, new BecomeTrainerFormModel
                {
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    ImageUrl = imageUrl,
                    Description = description,
                    SportId = sportId
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TrainersController>(c => c.All(With.Any<AllTrainersQueryModel>())));

        [Fact]
        public void DeleteShouldBeAuthorizedAndReturnRedirect()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer())
                    .WithData(TenPublicSports)
                    .WithData(User())
                    .WithUser())
               .Calling(c => c.Delete(1))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TrainersController>(c => c.All(With.Any<AllTrainersQueryModel>())));

        [Fact]
        public void MyProfileShouldReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer())
                    .WithData(TenPublicSports)
                    .WithData(User())
                    .WithUser())
                .Calling(c => c.MyProfile("TestId"))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<TrainerDetailsViewModel>());

        [Fact]
        public void HireShouldReturnRedirect()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(SecondTrainer())
                    .WithData(TenPublicSports)
                    .WithData(User())
                    .WithData(SecondUser())
                    .WithUser())
                .Calling(c => c.Hire(2))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TrainersController>(c => c.All(With.Any<AllTrainersQueryModel>())));


        private static AllTrainersQueryModel GetQuery()
        {
            AllTrainersQueryModel model = new AllTrainersQueryModel
            {
                SearchTerm = null,
                Sport = null,
                Sorting = TrainerSorting.DateCreated,
                CurrentPage = 1
            };

            return model;
        }

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

        private static Trainer SecondTrainer()
        {
            var trainer = new Trainer()
            {
                Id = 2,
                FullName = "Timenov Pavel",
                Email = "paveltimenov@gmail.bg",
                PhoneNumber = "87452136586",
                ImageUrl = "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
                Description = "Hello. I am Timenov Pavel. Taekwondo black belt.",
                SportId = 1,
                Sport = new Sport
                {
                    Name = "Football",
                    Origin = "England",
                    Description = "The best game. It is the most played in the world."
                },
                UserId = "SecondId",
                IsPublic = true
            };

            return trainer;
        }

        private static string Sport()
        {
            var trainer = Trainer();

            var sport = trainer.Sport.Name;

            return sport;
        }

        private static User User()
        {
            var user = new User
            {
                Id = "TestId",
                UserName = "TestUser",               
            };

            return user;
        }

        private static User SecondUser()
        {
            var user = new User
            {
                Id = "SecondId",
                UserName = "SecondUserName",
            };

            return user;
        }
    }
}
