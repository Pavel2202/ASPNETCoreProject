namespace FitnessSite.Services.Trainers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Trainers;
    using System.Collections.Generic;
    using System.Linq;

    public class TrainersService : ITrainersService
    {
        private readonly FitnessSiteDbContext context;
        private readonly IMapper mapper;

        public TrainersService(FitnessSiteDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<TrainerSportsViewModel> AllSports()
            => context.Sports
                .ProjectTo<TrainerSportsViewModel>(mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<TrainerListingViewModel> AllTrainers(AllTrainersQueryModel query)
        {
            var trainersQuery = context.Trainers
                .Where(t => t.IsPublic)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Sport))
            {
                trainersQuery = trainersQuery.Where(t =>
                    t.Sport.Id == int.Parse(query.Sport));
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                trainersQuery = trainersQuery.Where(t =>
                    t.FullName.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    t.Sport.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            trainersQuery = query.Sorting switch
            {
                TrainerSorting.FullName => trainersQuery.OrderByDescending(t => t.FullName),
                TrainerSorting.Customers => trainersQuery.OrderByDescending(t => t.Customers.Count),
                TrainerSorting.Sport => trainersQuery.OrderByDescending(t => t.Sport.Name),
                TrainerSorting.DateCreated or _ => trainersQuery.OrderByDescending(t => t.Id)
            };

            var trainers = trainersQuery
                .Skip((query.CurrentPage - 1) * AllTrainersQueryModel.TrainersPerPage)
                .Take(AllTrainersQueryModel.TrainersPerPage)
                .ProjectTo<TrainerListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return trainers;
        }

        public bool Create(BecomeTrainerFormModel model, string userId)
        {
            if (context.Trainers.Any(t => t.UserId == userId))
            {
                return false;
            }

            var trainer = mapper.Map<Trainer>(model);

            trainer.UserId = userId;

            context.Trainers.Add(trainer);
            context.SaveChanges();

            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            user.TrainerId = trainer.Id;

            context.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var trainer = context.Trainers.FirstOrDefault(t => t.Id == id);

            var user = context.Users
                .FirstOrDefault(u => u.TrainerId == trainer.Id);

            context.Trainers.Remove(trainer);

            user.TrainerId = null;

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

            var model = mapper.Map<BecomeTrainerFormModel>(trainer);

            return model;
        }

        public TrainerDetailsViewModel GetTrainer(int id)
        {
            var trainer = context.Trainers
                .Where(t => t.Id == id)
                .ProjectTo<TrainerDetailsViewModel>(mapper.ConfigurationProvider)
                .First();

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
