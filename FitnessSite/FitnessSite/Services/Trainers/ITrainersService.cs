﻿namespace FitnessSite.Services.Trainers
{
    using FitnessSite.Models.Trainers;
    using System.Collections.Generic;

    public interface ITrainersService
    {
        IEnumerable<TrainerSportsViewModel> AllSports();

        bool Create(BecomeTrainerFormModel model, string userId);
    }
}
