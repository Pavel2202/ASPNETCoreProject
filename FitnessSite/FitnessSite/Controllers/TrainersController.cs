namespace FitnessSite.Controllers
{
    using FitnessSite.Models.Trainers;
    using FitnessSite.Services.Trainers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class TrainersController : Controller
    {
        private readonly ITrainersService service;

        public TrainersController(ITrainersService service)
        {
            this.service = service;
        }

        public IActionResult All([FromQuery] AllTrainersQueryModel query)
        {
            var trainers = service.AllTrainers(query);

            var trainersCount = service.TotalTrainers();

            return this.View(new AllTrainersQueryModel
            {
                SearchTerm = query.SearchTerm,
                Sorting = query.Sorting,
                TotalTrainers = trainersCount,
                Trainers = trainers
            });
        }

        [Authorize]
        public IActionResult Become()
        {
            var sports = service.AllSports();

            return this.View(new BecomeTrainerFormModel
            {
                Sports = sports
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeTrainerFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = service.Create(model, userId);

            if (!created)
            {
                return BadRequest();
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
