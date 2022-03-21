namespace FitnessSite.Models.Trainers
{
    public class TrainerListingViewModel : ITrainerModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string Sport { get; set; }

        public bool IsPublic { get; set; }
    }
}
