namespace FitnessSite.Models.Sports
{
    public class SportsListingViewModel : ISportsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Origin { get; set; }

        public bool IsPublic { get; set; }
    }
}
