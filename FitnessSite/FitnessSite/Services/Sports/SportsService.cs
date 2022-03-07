namespace FitnessSite.Services.Sports
{
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;
    using System.Linq;

    public class SportsService : ISportsService
    {
        private readonly ApplicationDbContext context;

        public SportsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<SportsViewModel> All()
            => context.Sports
            .Select(s => new SportsViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Origin = s.Origin
            })
            .ToList();

        public void Create(SportsFormModel model)
        {
            var sport = new Sport()
            {
                Name = model.Name,
                Origin = model.Origin,
                Description = model.Description
            };

            context.Sports.Add(sport);

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var sport = context.Sports
                .FirstOrDefault(s => s.Id == id);

            context.Sports.Remove(sport);

            context.SaveChanges();
        }

        public bool Edit(int id, SportsFormModel model)
        {
            var sport = context.Sports
                .Where(s => s.Id == id).FirstOrDefault();

            if (sport is null)
            {
                return false;
            }

            sport.Name = model.Name;
            sport.Origin = model.Origin;
            sport.Description = model.Description;

            context.SaveChanges();

            return true;
        }

        public SportsFormModel EditConvert(SportsDetailsViewModel sport)
        {
            var model = new SportsFormModel
            {
                Name = sport.Name,
                Origin = sport.Origin,
                Description = sport.Description
            };

            return model;
        }

        public SportsDetailsViewModel GetSport(int id)
            => context.Sports
                .Where(s => s.Id == id)
                .Select(s => new SportsDetailsViewModel
                {
                    Name = s.Name,
                    Origin = s.Origin,
                    Description = s.Description
                }).First();
    }
}
