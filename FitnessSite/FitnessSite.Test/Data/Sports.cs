namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;
    using System.Linq;

    public static class Sports
    {
        public static IEnumerable<Sport> TenPublicSports
            => Enumerable.Range(0, 10).Select(p => new Sport
            {
                IsPublic = true
            });

        public static AllSportsQueryModel GetQuery
            =>new AllSportsQueryModel
            {
                SearchTerm = null,
                Sorting = SportSorting.DateCreated,
                CurrentPage = 1
            };

        public static Sport Sport
            =>new Sport()
            {
                Id = 1,
                Name = "Football",
                Origin = "England",
                Description = "The most played sport in the world. It is also the most expensive sport.",
                IsPublic = true
            };
    }
}
