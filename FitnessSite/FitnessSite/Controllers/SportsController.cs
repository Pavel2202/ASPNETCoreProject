namespace FitnessSite.Controllers
{
    using FitnessSite.Infrastructure;
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
        {
            if (!this.User.IsAdmin())
            {
                return BadRequest();
            }

            return this.View();
        }

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

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.User.IsAdmin())
            {
                return BadRequest();
            }

            var sport = service.GetSport(id);

            var model = service.EditConvert(sport);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, SportsFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var edited = service.Edit(id, model);

            if (!edited)
            {
                return BadRequest();
            }

            return this.RedirectToAction("Details", new { id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.User.IsAdmin())
            {
                return BadRequest();
            }

            service.Delete(id);

            return this.RedirectToAction("All", "Sports");
        }
    }
}
