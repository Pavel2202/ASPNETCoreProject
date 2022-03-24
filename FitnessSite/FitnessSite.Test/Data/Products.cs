namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Products
    {
        public static IEnumerable<Product> TenPublicProducts
            => Enumerable.Range(0, 10).Select(p => new Product
            {
                IsPublic = true
            });
    }
}
