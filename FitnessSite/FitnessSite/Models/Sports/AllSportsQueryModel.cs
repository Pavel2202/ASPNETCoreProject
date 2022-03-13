namespace FitnessSite.Models.Sports
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllSportsQueryModel
    {
        public const int SportsPerPage = 12;

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public SportSorting Sorting { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int TotalSports { get; set; }

        public IEnumerable<SportsListingViewModel> Sports { get; set; }
    }
}
