namespace FitnessSite.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Sports;
    using FitnessSite.Data.Models;
    using System.Linq;

    using static Data.Sports;

    using static Areas.Admin.AdminConstants;
    using static WebConstants;

    public class SportsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(TenPublicSports))
                .Calling(c => c.All(GetQuery()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllSportsQueryModel>());

        [Fact]
        public void GetAddShouldBeForAdminsAndReturnView()
            => MyController<SportsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Football",
            "England",
            "The most played sport in the world. It is also the most expensive sport.")]
        public void PostAddShouldBeForAdminsAndReturnRedirectWithValidModel(
            string name,
            string origin,
            string description)
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new SportsFormModel
                {
                    Name = name,
                    Origin = origin,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Sport>(sports => sports
                        .Any(s =>
                            s.Name == name &&
                            s.Origin == origin &&
                            s.Description == description)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<SportsController>(c => c.All(With.Any<AllSportsQueryModel>())));

        [Theory]
        [InlineData("fo",
            "England",
            "The most played sport in the world. It is also the most expensive sport.")]
        public void PostAddShouldBeForAdminsAndReturnViewWhenModelIsInvalid(
            string name,
            string origin,
            string description)
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new SportsFormModel
                {
                    Name = name,
                    Origin = origin,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SportsFormModel>());

        [Fact]
        public void DetailsShouldReturnView()
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport()))
                .Calling(c => c.Details(1, "Football-England"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SportsDetailsViewModel>());

        [Fact]
        public void DetailsShouldReturnBadRequestWhenInformationIsInvalid()
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport()))
                .Calling(c => c.Details(1, "Football-France"))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void GetEditShouldBeForAdminsAndReturnView()
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport()))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(1,
            "Football",
            "England",
            "The most played sport in the world. It is also the most expensive sport.")]
        public void PostEditShouldBeForAdminsAndReturnRedirectWithValidModel(
            int id,
            string name,
            string origin,
            string description)
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport())
                    .WithUser())
                .Calling(c => c.Edit(id, new SportsFormModel
                {
                    Name = name,
                    Origin = origin,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<SportsController>(c => c.Details(id, "Football-England")));

        [Theory]
        [InlineData(1,
            "fo",
            "England",
            "The most played sport in the world. It is also the most expensive sport.")]
        public void PostEditShouldBeForAdminsAndReturnViewWhenModelIsInvalid(
            int id,
            string name,
            string origin,
            string description)
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport())
                    .WithUser())
                .Calling(c => c.Edit(id, new SportsFormModel
                {
                    Name = name,
                    Origin = origin,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SportsFormModel>());

        [Theory]
        [InlineData(2,
            "Football",
            "England",
            "The most played sport in the world. It is also the most expensive sport.")]
        public void PostEditShouldBeForAdminsAndReturnBadRequestWhenIdIsInvalid(
            int id,
            string name,
            string origin,
            string description)
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport())
                    .WithUser())
                .Calling(c => c.Edit(id, new SportsFormModel
                {
                    Name = name,
                    Origin = origin,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldBeForAdminsAndReturnRedirect()
            => MyController<SportsController>
                .Instance(controller => controller
                    .WithData(Sport())
                    .WithUser())
               .Calling(c => c.Delete(1))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<SportsController>(c => c.All(With.Any<AllSportsQueryModel>())));

        private static AllSportsQueryModel GetQuery()
        {
            AllSportsQueryModel model = new AllSportsQueryModel
            {
                SearchTerm = null,
                Sorting = SportSorting.DateCreated,
                CurrentPage = 1
            };

            return model;
        }

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
