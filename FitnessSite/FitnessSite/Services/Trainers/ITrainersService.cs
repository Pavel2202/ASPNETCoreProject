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

        TrainerDetailsViewModel GetTrainer(int id);

        bool Hire(int trainerId, string userId);

        string MyProfile(string userId);

        bool IsTrainer(int trainerId, string userId);

        bool Edit(int id, BecomeTrainerFormModel model);

        BecomeTrainerFormModel EditConvert(int id);

        void Delete(int id);

        bool IsUserTrainer(string userId);
    }
}
