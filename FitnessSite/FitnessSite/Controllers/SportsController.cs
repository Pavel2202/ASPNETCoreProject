namespace FitnessSite.Controllers
{
    using FitnessSite.Models.Sports;
    using FitnessSite.Services.Sports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SportsController : Controller
    {
        private readonly ISportsService service;

        public SportsController(ISportsService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            var sports = service.All();

            return this.View(sports);
        }

        [Authorize]
        public IActionResult Add()
            => this.View();

        [HttpPost]
        [Authorize]
        public IActionResult Add(SportsFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            service.Create(model);

            return this.RedirectToAction("All", "Sports");
        }

        public IActionResult Details(int id)
        {
            var sport = service.GetSport(id);

            return this.View(sport);
        }
    }
}
