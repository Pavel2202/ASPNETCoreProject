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
                Description = model.Description,
                SportId = model.SportId,
                UserId = userId
            };

            context.Trainers.Add(trainer);
            context.SaveChanges();

            return true;
        }
    }
}
