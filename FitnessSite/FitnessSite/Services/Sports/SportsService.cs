namespace FitnessSite.Services.Sports
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FitnessSite.Data;
    using FitnessSite.Data.Models;
    using FitnessSite.Models.Sports;
    using System.Collections.Generic;
    using System.Linq;

    public class SportsService : ISportsService
    {
        private readonly FitnessSiteDbContext context;
        private readonly IMapper mapper;

        public SportsService(FitnessSiteDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<SportListingViewModel> All(
            string searchTerm = null,
            SportSorting sorting = SportSorting.DateCreated,
            int currentPage = 1,
            int sportsPerPage = int.MaxValue,
            bool isPublic = true)
        {
            var sportsQuery = context.Sports
                .Where(s => !isPublic || s.IsPublic)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sportsQuery = sportsQuery.Where(s =>
                    s.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    s.Origin.ToLower().Contains(searchTerm.ToLower()));
            }

            sportsQuery = sorting switch
            {
                SportSorting.Name => sportsQuery.OrderBy(r => r.Name),
                SportSorting.Origin => sportsQuery.OrderBy(r => r.Origin),
                SportSorting.DateCreated or _ => sportsQuery.OrderByDescending(r => r.Id)
            };

            var sports = sportsQuery
                .Skip((currentPage - 1) * AllSportsQueryModel.SportsPerPage)
                .Take(AllSportsQueryModel.SportsPerPage)
                .ProjectTo<SportListingViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return sports;
        }

        public void ChangeVisibility(int id)
        {
            var sport = context.Sports
                .FirstOrDefault(s => s.Id == id);

            var state = sport.IsPublic;

            sport.IsPublic = !state;

            context.SaveChanges();
        }

        public void Create(SportFormModel model)
        {
            var sport = mapper.Map<Sport>(model);

            sport.IsPublic = true;

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

        public bool Edit(int id, SportFormModel model)
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
            sport.IsPublic = false;

            context.SaveChanges();

            return true;
        }

        public SportFormModel EditConvert(SportDetailsViewModel sport)
        {
            var model = mapper.Map<SportFormModel>(sport);

            return model;
        }

        public SportDetailsViewModel GetSport(int id)
            => context.Sports
                .Where(s => s.Id == id)
                .ProjectTo<SportDetailsViewModel>(mapper.ConfigurationProvider)
                .First();

        public int TotalSports()
            => context.Sports.Where(s => s.IsPublic).Count();

        public int TotalSportsAdminArea()
            => context.Sports.Count();
    }
}
