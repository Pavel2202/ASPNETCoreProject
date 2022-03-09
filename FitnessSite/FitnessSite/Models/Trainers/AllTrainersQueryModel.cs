namespace FitnessSite.Models.Trainers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllTrainersQueryModel
    {
        public const int TrainersPerPage = 6;

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public TrainerSorting Sorting { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int TotalTrainers { get; set; }

        public IEnumerable<TrainerListingViewModel> Trainers { get; set; }
    }
}
