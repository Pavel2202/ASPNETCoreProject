﻿namespace FitnessSite.Controllers
{
    using AutoMapper;
    using FitnessSite.Models;
    using FitnessSite.Models.Home;
    using FitnessSite.Models.Products;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Models.Trainers;
    using FitnessSite.Services.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using static FitnessSite.WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IHomeService service;
        private readonly IMemoryCache cache;

        public HomeController(IHomeService service, IMemoryCache cache, IMapper mapper)
        {
            this.service = service;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));

            var trainer = this.cache.Get<TrainerListingViewModel>(BestTrainerCacheKey);

            if (trainer == null)
            {
                trainer = service.BestTrainer();

                this.cache.Set(BestTrainerCacheKey, trainer, cacheOptions);
            }

            var recipe = this.cache.Get<RecipeListingViewModel>(DailyRecipeCacheKey);

            if (recipe == null)
            {
                recipe = service.DailyRecipe();

                this.cache.Set(DailyRecipeCacheKey, recipe, cacheOptions);
            }

            var product = this.cache.Get<ProductListingViewModel>(DailyProductCacheKey);

            if (product == null)
            {
                product = service.DailyProduct();

                this.cache.Set(DailyProductCacheKey, product, cacheOptions);
            }

            var model = new IndexDisplayViewModel
            {
                Recipe = recipe,
                Product = product,
                Trainer = trainer
            };

            return View(model);
        }

        public IActionResult Error()
            => this.View();
    }
}
