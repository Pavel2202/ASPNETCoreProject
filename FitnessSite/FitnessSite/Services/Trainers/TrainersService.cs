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
                .Where(s => s.IsPublic)
                .ProjectTo<TrainerSportsViewModel>(mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<TrainerListingViewModel> All(
            string searchTerm = null,
            string sport = null,
            TrainerSorting sorting = TrainerSorting.DateCreated,
            int currentPage = 1,
            int trainersPerPage = int.MaxValue,
            bool isPublic = true)
        {
            var trainersQuery = context.Trainers
                .Where(t => (!isPublic || t.IsPublic) && t.UserId != null)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sport))
            {
                trainersQuery = trainersQuery.Where(t =>
                    t.Sport.Id == int.Parse(sport));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                trainersQuery = trainersQuery.Where(t =>
                    t.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                    t.Sport.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            trainersQuery = sorting switch
            {
                TrainerSorting.FullName => trainersQuery.OrderBy(t => t.FullName),
                TrainerSorting.Customers => trainersQuery.OrderByDescending(t => t.Customers.Count),
                TrainerSorting.Sport => trainersQuery.OrderBy(t => t.Sport.Name),
                TrainerSorting.DateCreated or _ => trainersQuery.OrderByDescending(t => t.Id)
            };

            var trainers = trainersQuery
                .Skip((currentPage - 1) * AllTrainersQueryModel.TrainersPerPage)
                .Take(AllTrainersQueryModel.TrainersPerPage)
                .ProjectTo<TrainerListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return trainers;
        }

        public void ChangeVisibility(int id)
        {
            var trainer = context.Trainers
                .FirstOrDefault(t => t.Id == id);

            var state = trainer.IsPublic;

            trainer.IsPublic = !state;

            context.SaveChanges();
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
                .FirstOrDefault(u => u.Id == trainer.UserId);

            user.TrainerId = null;

            trainer.UserId = null;

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
            trainer.IsPublic = false;

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

            if (trainer.UserId == userId)
            {
                return false;
            }

            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (trainer.Customers.Any(u => u.Id == userId))
            {
                return false;
            }

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
            => context.Trainers.Where(t => t.UserId != null && t.IsPublic).Count();

        public int TotalTrainersAdminArea()
            => context.Trainers.Where(t => t.UserId != null).Count();
    }
}
