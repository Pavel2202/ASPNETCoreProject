namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Sports
    {
        public static IEnumerable<Sport> TenPublicSports
            => Enumerable.Range(0, 10).Select(p => new Sport
            {
                IsPublic = true
            });
    }
}
