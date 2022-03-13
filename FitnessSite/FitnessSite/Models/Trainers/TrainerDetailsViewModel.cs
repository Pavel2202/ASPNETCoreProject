namespace FitnessSite.Models.Trainers
{
    public class TrainerDetailsViewModel : ITrainerModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Sport { get; set; }
    }
}
