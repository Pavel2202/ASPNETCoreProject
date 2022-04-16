namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Infrastructure.Extensions;
    using FitnessSite.Models.Trainers;
    using FitnessSite.Services.Trainers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class TrainersController : Controller
    {
        private readonly ITrainersService service;
        private readonly IMapper mapper;

        public TrainersController(ITrainersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllTrainersQueryModel query)
        {
            var trainers = service.All(
                query.SearchTerm,
                query.Sport,
                query.Sorting,
                query.CurrentPage,
                AllTrainersQueryModel.TrainersPerPage);

            var trainersCount = service.TotalTrainers();

            var sports = service.AllSports();

            var trainersForm = mapper.Map<AllTrainersQueryModel>(query);

            trainersForm.Sports = sports;
            trainersForm.Trainers = trainers;
            trainersForm.TotalTrainers = trainersCount;

            return this.View(trainersForm);
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
                return this.View(model);
            }

            var created = service.Create(model, this.User.Id());

            if (!created)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "Thank you for becoming a trainer! Waiting for admin approval...";

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
        public IActionResult Edit(int id)
        {
            if (!service.IsTrainer(id, this.User.Id()) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            var trainer = service.ConvertToFormModel(id);

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

            TempData[GlobalMessageKey] = "You successfully edited a trainer!";

            return this.RedirectToAction("All", "Trainers");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!service.IsTrainer(id, this.User.Id()) && !this.User.IsAdmin())
            {
                return BadRequest();
            }

            service.Delete(id);

            TempData[GlobalMessageKey] = "You successfully deleted a trainer!";

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
        public IActionResult Hire(int id)
        {
            var hire = service.Hire(id, this.User.Id());

            if (!hire)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = "You successfully hired a trainer!";

            return this.RedirectToAction("All", "Trainers");
        }       
    }
}
