namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Sports;
    using FitnessSite.Services.Sports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Areas.Admin.AdminConstants;
    using static WebConstants;

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
            var sports = service.All(
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllSportsQueryModel.SportsPerPage
                );

            var totalSports = service.TotalSports();

            var sportsModel = this.mapper.Map<AllSportsQueryModel>(query);

            sportsModel.Sports = sports;
            sportsModel.TotalSports = totalSports;

            return this.View(sportsModel);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add()
            => this.View();

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(SportFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            service.Create(model);

            TempData[GlobalMessageKey] = "You successfully added a sport!";

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

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var sport = service.GetSport(id);

            var model = service.EditConvert(sport);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, SportFormModel model)
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

            TempData[GlobalMessageKey] = "You successfully edited a sport!";

            return this.RedirectToAction("Details", new { id, information = model.SportInformation() });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            service.Delete(id);

            TempData[GlobalMessageKey] = "You successfully deleted a sport!";

            return this.RedirectToAction("All", "Sports");
        }
    }
}
