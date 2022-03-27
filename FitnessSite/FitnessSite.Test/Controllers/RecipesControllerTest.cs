﻿namespace FitnessSite.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FitnessSite.Controllers;
    using FitnessSite.Models.Recipes;
    using FitnessSite.Data.Models;
    using System.Linq;
    using System.Collections.Generic;

    using static Data.Recipes;

    using static WebConstants;

    public class RecipesControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(TenPublicRecipes))
                .Calling(c => c.All(GetQuery()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllRecipesQueryModel>());

        [Fact]
        public void GetAddShouldBeAuthorizedAndReturnView()
            => MyController<RecipesController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Scrambled eggs",
            "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
            "Crack the eggs. Put then in the pan and mix.")]
        public void PostAddShouldBeAuthorizedAndReturnRedirectWithValidModel(string title,
            string imageUrl,
            string description)
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new RecipeFormModel
                {
                    Title = title,
                    ImageUrl = imageUrl,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Recipe>(recipes => recipes
                        .Any(r => 
                            r.Title == title &&
                            r.ImageUrl == imageUrl &&
                            r.Description == description)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<RecipesController>(c => c.All(With.Any<AllRecipesQueryModel>())));

        [Theory]
        [InlineData("eg",
            "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
            "Crack the eggs. Put then in the pan and mix.")]
        public void PostAddShouldBeAuthorizedAndReturnViewWhenModelIsInvalid(string title,
            string imageUrl,
            string description)
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new RecipeFormModel
                {
                    Title = title,
                    ImageUrl = imageUrl,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<RecipeFormModel>());

        [Fact]
        public void DetailsShouldReturnView()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
                .Calling(c => c.Details(1, "Scrambled eggs"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<RecipeDetailsViewModel>());

        [Fact]
        public void DetailsShouldReturnBadRequestWhenInformationIsInvalid()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
                .Calling(c => c.Details(1, "Rice"))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void GetEditShouldBeAuthorizedAndReturnView()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetEditShouldBeAuthorizedAndReturnUnauthorizedWhenUserIsNotCreatorOrAdmin()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(SecondRecipe())
                    .WithUser())
                .Calling(c => c.Edit(2))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Theory]
        [InlineData(1,
            "Scrambled eggs",
            "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
            "Crack the eggs. Put then in the pan and mix.")]
        public void PostEditShouldBeAuthorizedAndReturnRedirectWithValidModel(int id,
            string title,
            string imageUrl,
            string description)
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
                .Calling(c => c.Edit(id, new RecipeFormModel
                {
                    Title = title,
                    ImageUrl = imageUrl,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<RecipesController>(c => c.Details(id, title)));

        [Theory]
        [InlineData(1,
            "eg",
            "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
            "Crack the eggs. Put then in the pan and mix.")]
        public void PostEditShouldBeAuthorizedAndReturnViewWhenModelIsInvalid(int id,
            string title,
            string imageUrl,
            string description)
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
                .Calling(c => c.Edit(id, new RecipeFormModel
                {
                    Title = title,
                    ImageUrl = imageUrl,
                    Description = description
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<RecipeFormModel>());

        [Theory]
        [InlineData(2,
            "Scrambled eggs",
            "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
            "Crack the eggs. Put then in the pan and mix.")]
        public void PostEditShouldBeAuthorizedAndReturnBadRequestWhenIdIsInvalid(int id,
            string title,
            string imageUrl,
            string description)
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
                .Calling(c => c.Edit(id, new RecipeFormModel
                {
                    Title = title,
                    ImageUrl = imageUrl,
                    Description = description
                }))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldBeAuthorizedAndReturnRedirect()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(Recipe())
                    .WithUser())
               .Calling(c => c.Delete(1))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<RecipesController>(c => c.All(With.Any<AllRecipesQueryModel>())));

        [Fact]
        public void DeleteShouldBeAuthorizedAndReturnUnauthorizedWhenUserIsNotCreatorOrAdmin()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithData(SecondRecipe())
                    .WithUser())
                .Calling(c => c.Delete(2))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void MineShouldReturnView()
            => MyController<RecipesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Mine())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<RecipeListingViewModel>>());

        private static AllRecipesQueryModel GetQuery()
        {
            AllRecipesQueryModel model = new AllRecipesQueryModel
            {
                SearchTerm = null,
                Sorting = RecipeSorting.DateCreated,
                CurrentPage = 1
            };

            return model;
        }

        private static Recipe Recipe()
        {
            var recipe = new Recipe()
            {
                Id = 1,
                Title = "Scrambled eggs",
                ImageUrl = "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
                Description = "Crack the eggs. Put then in the pan and mix.",
                Creator = new User()
                {
                    Id = "TestId",
                    UserName = "TestUser"
                },
                IsPublic = true
            };

            return recipe;
        }

        private static Recipe SecondRecipe()
        {
            var recipe = new Recipe()
            {
                Id = 2,
                Title = "Rice",
                ImageUrl = "https://bakeitwithlove.com/wp-content/uploads/2021/12/scrambled-eggs-sq.jpg",
                Description = "Crack the eggs. Put then in the pan and mix.",
                Creator = new User()
                {
                    Id = "InvalidId",
                    UserName = "InvalidUserName"
                },
                IsPublic = true
            };

            return recipe;
        }
    }
}
