namespace FitnessSite.Services.Sports
{
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;

    public interface ISportsService
    {
        IEnumerable<SportListingViewModel> All(string searchTerm = null,
            SportSorting sorting = SportSorting.DateCreated,
            int currentPage = 1,
            int sportsPerPage = int.MaxValue,
            bool isPublic = true);

        int TotalSports();

        int TotalSportsAdminArea();

        void Create(SportFormModel model);

        SportDetailsViewModel GetSport(int id);

        SportFormModel EditConvert(SportDetailsViewModel sport);

        bool Edit(int id, SportFormModel model);

        void Delete(int id);

        void ChangeVisibility(int id);
    }
}
