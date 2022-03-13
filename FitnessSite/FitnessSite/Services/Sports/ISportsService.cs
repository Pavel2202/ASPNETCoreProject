namespace FitnessSite.Services.Sports
{
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;

    public interface ISportsService
    {
        IEnumerable<SportsListingViewModel> All();

        void Create(SportsFormModel model);

        SportsDetailsViewModel GetSport(int id);

        SportsFormModel EditConvert(SportsDetailsViewModel sport);

        bool Edit(int id, SportsFormModel model);

        void Delete(int id);
    }
}
