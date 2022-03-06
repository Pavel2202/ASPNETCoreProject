﻿namespace FitnessSite.Services.Sports
{
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;

    public interface ISportsService
    {
        IEnumerable<SportsViewModel> All();

        void Create(SportsFormModel model);

        SportsDetailsViewModel GetSport(int id);
    }
}
