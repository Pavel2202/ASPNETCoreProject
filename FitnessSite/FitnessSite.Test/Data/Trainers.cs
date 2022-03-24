namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Trainers
    {
        public static IEnumerable<Trainer> TenPublicTrainers
            => Enumerable.Range(0, 10).Select(p => new Trainer
            {
                IsPublic = true
            });

        private static int RandomCustomers()
        {
            Random random = new Random();

            var result = random.Next(1, 20);

            return result;
        }
    }
}
