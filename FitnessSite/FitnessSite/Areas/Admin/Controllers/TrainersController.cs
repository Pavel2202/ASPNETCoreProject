namespace FitnessSite.Areas.Admin.Controllers
{
    using FitnessSite.Models.Trainers;
    using FitnessSite.Services.Trainers;
    using Microsoft.AspNetCore.Mvc;

    public class TrainersController : AdminController
    {
        private readonly ITrainersService service;

        public TrainersController(ITrainersService service)
        {
            this.service = service;
        }

        public IActionResult All(string currentPage)
        {
            var totalTrainers = service.TotalTrainers();

            int page = 1;

            if (currentPage != null)
            {
                page = int.Parse(currentPage);
            }

            var trainers = new AllTrainersQueryModel
            {
                CurrentPage = page,
                TotalTrainers = totalTrainers
            };

            trainers.Trainers = service.AllTrainers(null,
                null,
                TrainerSorting.DateCreated,
                trainers.CurrentPage,
                isPublic: false);

            return this.View(trainers);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.service.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
