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

            var sports = service.AllSports();

            return this.View(new AllTrainersQueryModel
            {
                SearchTerm = query.SearchTerm,
                Sorting = query.Sorting,
                TotalTrainers = trainersCount,
                Sports = sports,
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

            return this.RedirectToAction("All", "Trainers");
        }

        public IActionResult Details(int id)
        {
            var trainer = service.GetTrainer(id);

            return this.View(trainer);
        }

        [Authorize]
        public IActionResult Hire(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var hire = service.Hire(id, userId);

            if (!hire)
            {
                return BadRequest();
            }

            return this.RedirectToAction("All", "Trainers");
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainerId = service.MyProfile(userId);

            if (trainerId is null)
            {
                return this.RedirectToAction("Become", "Trainers");
            }

            var trainer = service.GetTrainer(int.Parse(trainerId));

            return this.View(trainer);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!service.IsTrainer(id, userId))
            {
                return BadRequest();
            }

            var trainer = service.EditConvert(id);

            trainer.Sports = service.AllSports();

            return this.View(trainer);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BecomeTrainerFormModel model)
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

            return this.RedirectToAction("MyProfile", "Trainers");
        }

        public IActionResult Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!service.IsTrainer(id, userId))
            {
                return BadRequest();
            }

            service.Delete(id);

            return this.RedirectToAction("All", "Trainers");
        }
    }
}
