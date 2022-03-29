namespace FitnessSite.Test.Controllers
{
    using FitnessSite.Controllers;
    using FitnessSite.Models.Trainers;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;
    using static Data.Sports;
    using static Data.Trainers;
    using static WebConstants;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(TenPublicTrainers))
                .Calling(c => c.All(Data.Trainers.GetQuery))
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
                    .WithData(User)
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
                    .WithSet<FitnessSite.Data.Models.Trainer>(trainers => trainers
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

        [Theory]
        [InlineData("Pa",
            "paveltimenov@abv.bg",
            "514752368",
            "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
            "Hello. I am Pavel Timenov. Taekwondo black belt.",
            1)]
        public void PostBecomeShouldBeAuthorizedAndReturnViewWhenModelIsInvalid(
            string fullName,
            string email,
            string phoneNumber,
            string imageUrl,
            string description,
            int sportId)
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(TenPublicSports)
                    .WithData(User)
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
                .InvalidModelState()
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
        public void PostBecomeShouldBeAuthorizedAndReturnBadRequestWhenUserIsTrainerAlready(
            string fullName,
            string email,
            string phoneNumber,
            string imageUrl,
            string description,
            int sportId)
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(TenPublicSports)
                    .WithData(Trainer)
                    .WithData(User)
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
                .AndAlso()
                .ShouldReturn()
                .BadRequest();


        [Fact]
        public void DetailsShouldReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer))
                .Calling(c => c.Details(1, $"Pavel Timenov-{Sport()}"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<TrainerDetailsViewModel>());

        [Fact]
        public void DetailsShouldReturnBadRequestWhenInformationIsInvalid()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer))
                .Calling(c => c.Details(1, $"Pavel-{Sport()}"))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void GetEditShouldBeAuthorizedAndReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
                    .WithUser())
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetEditShouldBeAuthorizedAndReturnBadRequestWhenUserIsNotTrainerOrAdmin()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(SecondTrainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
                    .WithUser())
                .Calling(c => c.Edit(2))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

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
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
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

        [Theory]
        [InlineData(1,
            "Pa",
            "paveltimenov@abv.bg",
            "514752368",
            "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
            "Hello. I am Pavel Timenov. Taekwondo black belt.",
            1)]
        public void PostEditShouldBeAuthorizedAndReturnViewWhenModelIsInvalid(
            int id,
            string fullName,
            string email,
            string phoneNumber,
            string imageUrl,
            string description,
            int sportId)
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
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
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<BecomeTrainerFormModel>());

        [Theory]
        [InlineData(2,
            "Pavel Timenov",
            "paveltimenov@abv.bg",
            "514752368",
            "https://cdn.cnn.com/cnnnext/dam/assets/211020105902-01-conor-mcgregor-july21-large-169.jpg",
            "Hello. I am Pavel Timenov. Taekwondo black belt.",
            1)]
        public void PostEditShouldBeAuthorizedAndReturnBadRequestWhenIdIsInvalid(
            int id,
            string fullName,
            string email,
            string phoneNumber,
            string imageUrl,
            string description,
            int sportId)
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
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
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldBeAuthorizedAndReturnRedirect()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
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
        public void DeleteShouldBeAuthorizedAndReturnBadRequestWhenUserIsNotTrainerOrAdmin()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(SecondTrainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
                    .WithUser())
               .Calling(c => c.Delete(2))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void MyProfileShouldReturnView()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
                    .WithUser())
                .Calling(c => c.MyProfile())
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
                    .WithData(SecondTrainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
                    .WithData(SecondUser)
                    .WithUser())
                .Calling(c => c.Hire(2))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TrainersController>(c => c.All(With.Any<AllTrainersQueryModel>())));

        [Fact]
        public void HireShouldReturnBadRequestWhenUserIsCustomer()
            => MyController<TrainersController>
                .Instance(controller => controller
                    .WithData(Trainer)
                    .WithData(TenPublicSports)
                    .WithData(User)
                    .WithData(SecondUser)
                    .WithUser())
                .Calling(c => c.Hire(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest();
    }
}
