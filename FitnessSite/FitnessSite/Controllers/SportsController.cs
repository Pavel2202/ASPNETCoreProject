namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Sports;
    using FitnessSite.Services.Sports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SportsController : Controller
    {
        private readonly ISportsService service;
        private readonly IMapper mapper;

        public SportsController(ISportsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllSportsQueryModel query)
        {
            var sports = service.All(query);

            var totalSports = service.TotalSports();

            var sportsModel = this.mapper.Map<AllSportsQueryModel>(query);

            sportsModel.Sports = sports;
            sportsModel.TotalSports = totalSports;

            return this.View(sportsModel);
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

        public IActionResult Details(int id, string information)
        {
            var sport = service.GetSport(id);

            if (information != sport.SportInformation())
            {
                return BadRequest();
            }

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

            return this.RedirectToAction("Details", new { id, information = model.SportInformation() });
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
