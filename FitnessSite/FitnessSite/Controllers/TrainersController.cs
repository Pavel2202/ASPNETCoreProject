namespace FitnessSite.Controllers
{
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Trainers;
    using FitnessSite.Services.Trainers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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

            var created = service.Create(model, this.User.Id());

            if (!created)
            {
                return BadRequest();
            }

            return this.RedirectToAction("All", "Trainers");
        }

        public IActionResult Details(int id, string information)
        {
            var trainer = service.GetTrainer(id);

            if (information != trainer.TrainerInformation())
            {
                return BadRequest();
            }

            return this.View(trainer);
        }

        [Authorize]
        public IActionResult Hire(int id)
        {
            var hire = service.Hire(id, this.User.Id());

            if (!hire)
            {
                return BadRequest();
            }

            return this.RedirectToAction("All", "Trainers");
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var trainerId = service.MyProfile(this.User.Id());

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
            if (!service.IsTrainer(id, this.User.Id()) && !this.User.IsAdmin())
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
            if (!service.IsTrainer(id, this.User.Id()) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            service.Delete(id);

            return this.RedirectToAction("All", "Trainers");
        }
    }
}
