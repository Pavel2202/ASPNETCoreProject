namespace FitnessSite.Areas.Admin.Controllers
{
    using FitnessSite.Models.Sports;
    using FitnessSite.Services.Sports;
    using Microsoft.AspNetCore.Mvc;

    public class SportsController : AdminController
    {
        private readonly ISportsService service;

        public SportsController(ISportsService service)
        {
            this.service = service;
        }

        public IActionResult All(string currentPage)
        {
            var totalSports = service.TotalSportsAdminArea();

            int page = 1;

            if (currentPage != null)
            {
                page = int.Parse(currentPage);
            }

            var sports = new AllSportsQueryModel
            {
                CurrentPage = page,
                TotalSports = totalSports
            };

            sports.Sports = service.All(null,
                SportSorting.DateCreated,
                sports.CurrentPage,
                isPublic: false);

            return this.View(sports);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.service.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
