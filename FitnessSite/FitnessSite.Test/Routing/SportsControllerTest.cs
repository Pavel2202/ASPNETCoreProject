namespace FitnessSite.Test.Routing
{
    using FitnessSite.Controllers;
    using FitnessSite.Models.Sports;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SportsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Sports/All")
                .To<SportsController>(c => c.All(With.Any<AllSportsQueryModel>()));

        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Sports/Add")
                .To<SportsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Sports/Add")
                    .WithMethod(HttpMethod.Post))
                .To<SportsController>(c => c.Add(With.Any<SportFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Sports/Details")
                .To<SportsController>(c => c.Details(With.Any<int>(), With.Any<string>()));

        [Fact]
        public void GetEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Sports/Edit")
                .To<SportsController>(c => c.Edit(With.Any<int>()));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Sports/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<SportsController>(c => c.Edit(With.Any<int>(), With.Any<SportFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Sports/Delete")
                .To<SportsController>(c => c.Delete(With.Any<int>()));
    }
}
