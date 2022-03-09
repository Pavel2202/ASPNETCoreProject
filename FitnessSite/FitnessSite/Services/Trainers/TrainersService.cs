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

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                trainersQuery = trainersQuery.Where(r =>
                    r.FullName.ToLower().Contains(query.SearchTerm.ToLower()));
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

        public int TotalTrainers()
            => context.Trainers.Count();
    }
}
