namespace FitnessSite.Services.Sports
{
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;

    public interface ISportsService
    {
        IEnumerable<SportsListingViewModel> All(string searchTerm = null,
            SportSorting sorting = SportSorting.DateCreated,
            int currentPage = 1,
            int sportsPerPage = int.MaxValue,
            bool isPublic = true);

        int TotalSports();

        void Create(SportsFormModel model);

        SportsDetailsViewModel GetSport(int id);

        SportsFormModel EditConvert(SportsDetailsViewModel sport);

        bool Edit(int id, SportsFormModel model);

        void Delete(int id);

        void ChangeVisibility(int id);
    }
}
