namespace FitnessSite.Services.Trainers
{
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Trainers;
    using System.Collections.Generic;
    using System.Linq;

    public class TrainersService : ITrainersService
    {
        private readonly ApplicationDbContext context;

        public TrainersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TrainerSportsViewModel> AllSports()
            => context.Sports
            .Select(s => new TrainerSportsViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

        public IEnumerable<TrainerListingViewModel> AllTrainers(AllTrainersQueryModel query)
        {
            var trainersQuery = context.Trainers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Sport))
            {
                trainersQuery = trainersQuery.Where(t =>
                    t.Sport.Id == int.Parse(query.Sport));
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                trainersQuery = trainersQuery.Where(t =>
                    t.FullName.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            trainersQuery = query.Sorting switch
            {
                TrainerSorting.FullName => trainersQuery.OrderByDescending(t => t.FullName),
                TrainerSorting.Customers => trainersQuery.OrderByDescending(t => t.Customers.Count),
                TrainerSorting.DateCreated or _ => trainersQuery.OrderByDescending(t => t.Id)
            };

            var trainers = trainersQuery
                .Skip((query.CurrentPage - 1) * AllTrainersQueryModel.TrainersPerPage)
                .Take(AllTrainersQueryModel.TrainersPerPage)
                .Select(t => new TrainerListingViewModel
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Sport = t.Sport.Name,
                    ImageUrl = t.ImageUrl
                }).ToList();

            return trainers;
        }

        public bool Create(BecomeTrainerFormModel model, string userId)
        {
            if (context.Trainers.Any(t => t.UserId == userId))
            {
                return false;
            }

            var trainer = new Trainer()
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                SportId = model.SportId,
                UserId = userId
            };

            context.Trainers.Add(trainer);
            context.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var trainer = context.Trainers.FirstOrDefault(t => t.Id == id);

            context.Trainers.Remove(trainer);

            context.SaveChanges();
        }

        public bool Edit(int id, BecomeTrainerFormModel model)
        {
            var trainer = context.Trainers.FirstOrDefault(t => t.Id == id);

            if (trainer is null)
            {
                return false;
            }

            trainer.FullName = model.FullName;
            trainer.Email = model.Email;
            trainer.Description = model.Description;
            trainer.PhoneNumber = model.PhoneNumber;
            trainer.ImageUrl = model.ImageUrl;
            trainer.SportId = model.SportId;

            context.SaveChanges();

            return true;
        }

        public BecomeTrainerFormModel EditConvert(int id)
        {
            var trainer = context.Trainers
                .FirstOrDefault(t => t.Id == id);

            var model = new BecomeTrainerFormModel
            {
                FullName = trainer.FullName,
                Email = trainer.Email,
                ImageUrl = trainer.ImageUrl,
                PhoneNumber = trainer.PhoneNumber,
                Description = trainer.Description,
                SportId = trainer.SportId
            };

            return model;
        }

        public TrainersDetailsViewModel GetTrainer(int id)
        {
            var trainer = context.Trainers
                .Where(t => t.Id == id)
                .Select(t => new TrainersDetailsViewModel
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    ImageUrl = t.ImageUrl,
                    Description = t.Description,
                    Sport = t.Sport.Name
                }).First();

            return trainer;
        }

        public bool Hire(int trainerId, string userId)
        {
            var trainer = context.Trainers.FirstOrDefault(t => t.Id == trainerId);

            if (trainer.Customers.Any(t => t.Id == userId))
            {
                return false;
            }

            if (trainer.UserId == userId)
            {
                return false;
            }

            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            trainer.Customers.Add(user);

            context.SaveChanges();

            return true;
        }

        public bool IsTrainer(int trainerId, string userId)
        {
            var trainer = context.Trainers.FirstOrDefault(t => t.Id == trainerId);

            if (trainer.UserId == userId)
            {
                return true;
            }

            return false;
        }

        public bool IsUserTrainer(string userId)
        {
            var trainer = context.Trainers
                .FirstOrDefault(t => t.UserId == userId);

            if (trainer is null)
            {
                return false;
            }

            return true;
        }

        public string MyProfile(string userId)
        {
            var trainerId = context.Trainers.FirstOrDefault(t => t.UserId == userId).Id.ToString();

            if (trainerId is null)
            {
                return null;
            }

            return trainerId;
        }

        public int TotalTrainers()
            => context.Trainers.Count();
    }
}
