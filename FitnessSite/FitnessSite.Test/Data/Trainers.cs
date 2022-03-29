namespace FitnessSite.Test.Data
{
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Trainers;
    using System.Collections.Generic;
    using System.Linq;

    public static class Trainers
    {
        public static IEnumerable<Trainer> TenPublicTrainers
            => Enumerable.Range(0, 10).Select(p => new Trainer
            {
                IsPublic = true
            });

        public static AllTrainersQueryModel GetQuery
            => new AllTrainersQueryModel
            {
                SearchTerm = null,
                Sport = null,
                Sorting = TrainerSorting.DateCreated,
                CurrentPage = 1
            };

        public static Trainer Trainer
            => new Trainer()
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

        public static Trainer SecondTrainer
            => new Trainer()
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

        public static string Sport()
        {
            var trainer = Trainer;

            var sport = trainer.Sport.Name;

            return sport;
        }

        public static User User
            => new User
            {
                Id = "TestId",
                UserName = "TestUser",
            };

        public static User SecondUser
            => new User
            {
                Id = "SecondId",
                UserName = "SecondUserName",
            };
    }
}
