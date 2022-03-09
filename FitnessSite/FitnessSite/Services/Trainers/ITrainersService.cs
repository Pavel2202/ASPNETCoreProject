namespace FitnessSite.Services.Trainers
{
    using FitnessSite.Models.Trainers;
    using System.Collections.Generic;

    public interface ITrainersService
    {
        IEnumerable<TrainerSportsViewModel> AllSports();

        bool Create(BecomeTrainerFormModel model, string userId);

        IEnumerable<TrainerListingViewModel> AllTrainers(AllTrainersQueryModel query);

        int TotalTrainers();

        TrainersDetailsViewModel GetTrainer(int id);

        bool Hire(int trainerId, string userId);
    }
}
