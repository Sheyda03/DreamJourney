using AutoMapper;
using DreamJourney.Data;
using DreamJourney.Data.Models.Enums;
using DreamJourney.Filters;
using DreamJourney.Services.Interfaces;
using DreamJourney.ViewModels.Category;
using DreamJourney.ViewModels.Department;
using DreamJourney.ViewModels.Feature;
using DreamJourney.ViewModels.SubCategory;
using DreamJourney.ViewModels.Trip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamJourney.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService _tripsService;
        private readonly ICategoryService _categoriesService;
        private readonly IUserContextService _userContext;
        private readonly DreamJourneyDbContext _context;

        public TripsController(
            ITripsService tripsService,
            ICategoryService categoriesService,
            IUserContextService userContext,
            DreamJourneyDbContext context)
        {
            _tripsService = tripsService;
            _categoriesService = categoriesService;
            _userContext = userContext;
            _context = context;
        }
        [TripCreatorFilter]
        public async Task<IActionResult> TripsByCreator()
        {
            var trips = await _tripsService.GetTripsByCreatorAsync(_userContext.UserId);
            return View(trips);
        }

        public async Task<IActionResult> List(TripSearchViewModel model)
        {
            model = await _tripsService.SearchAsync(model);
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _tripsService.GetTripDetailsAsync(id);
            if (vm == null)
                return NotFound();

            vm.LoggedUserId = _userContext.UserId;
            vm.IsTripCreator = _userContext.IsTripCreator;
            vm.IsTraveller = !_userContext.IsTripCreator;
            vm.HasApplied = vm.TripApplications.Any(a =>
                            a.UserId == vm.LoggedUserId &&
                            (a.Status == ApplicationStatus.Pending ||
                             a.Status == ApplicationStatus.Accepted)
                        );

            vm.SimilarTrips = await _tripsService.GetSimilarTripsAsync(id);

            return View(vm);
        }
        [TripCreatorFilter]
        public async Task<IActionResult> Create()
        {
            var model = new TripEditViewModel();
            await LoadTripFormDataAsync(model);
            await LoadFeatureGroupsAsync(model);
            return View("Edit", model);
        }
        [HttpPost]
        [TripCreatorFilter]
        public async Task<IActionResult> Create(TripEditViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(Request.Form["SingleSelectedFeatureIds"]))
            {
                var radioIds = Request.Form["SingleSelectedFeatureIds"]
                    .ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();

                model.SelectedFeatureIds.AddRange(radioIds);
            }
            if (!ModelState.IsValid)
            {
                await LoadTripFormDataAsync(model);
                await LoadFeatureGroupsAsync(model);
                return View("Edit", model);
            }

            int newTripId = await _tripsService.CreateTripAsync(model, _userContext.UserId);
            return RedirectToAction("Details", new { id = newTripId });
        }
        [TripCreatorFilter]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _tripsService.GetTripForEditAsync(id, _userContext.UserId);
            if (model == null)
                return NotFound();

            await LoadTripFormDataAsync(model);
            await LoadFeatureGroupsAsync(model);
            return View(model);
        }

        [HttpPost]
        [TripCreatorFilter]
        public async Task<IActionResult> Edit(TripEditViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(Request.Form["SingleSelectedFeatureIds"]))
            {
                var radioIds = Request.Form["SingleSelectedFeatureIds"]
                    .ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();

                model.SelectedFeatureIds.AddRange(radioIds);
            }
            if (!ModelState.IsValid)
            {
                await LoadTripFormDataAsync(model);
                await LoadFeatureGroupsAsync(model);
                return View(model);
            }

            await _tripsService.UpdateTripAsync(model, _userContext.UserId);
            return RedirectToAction("TripsByCreator", new { id = model.Id });
        }

        [HttpPost]
        [TripCreatorFilter]
        public async Task<IActionResult> Delete(int id)
        {
            await _tripsService.DeleteTripAsync(id, _userContext.UserId);
            return RedirectToAction("List");
        }


        private async Task LoadTripFormDataAsync(TripEditViewModel model)
        {
            var categories = await _categoriesService.GetCategoriesAsync();
            model.Categories = categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            var subCategories = await _categoriesService.GetAllSubCategoriesAsync();
            model.SubCategories = subCategories
                .Select(sc => new SubCategoryViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToList();

            var departments = await _categoriesService.GetDepartmentsAsync();
            model.Departments = departments
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();
        }

        private async Task LoadFeatureGroupsAsync(TripEditViewModel model)
        {
            var featureCategories = await _context.FeatureCategories
                .Include(fc => fc.Features)
                .OrderBy(fc => fc.Id)
                .ToListAsync();

            model.FeatureGroups = featureCategories.Select(fc => new FeatureGroupViewModel
            {
                FeatureCategoryId = fc.Id,
                FeatureCategoryName = fc.Name,
                SingleSelection = fc.SingleSelection,
                Features = fc.Features
                    .OrderBy(f => f.Id)
                    .Select(f => new FeatureViewModel
                    {
                        Id = f.Id,
                        Name = f.Name,
                        FeatureCategoryId = fc.Id,
                        FeatureCategoryName = fc.Name
                    })
                    .ToList()
            }).ToList();

            model.SelectedFeatureIds ??= new List<int>();
        }
        [HttpGet]
        public async Task<IActionResult> GetSubCategoriesByCategory(int categoryId)
        {
            var subCategories = await _categoriesService.GetSubCategoriesByCategoryAsync(categoryId);

            var result = subCategories
                .Select(sc => new
                {
                    id = sc.Id,
                    name = sc.Name
                })
                .ToList();

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoriesByDepartment(int departmentId)
        {
            var categories = await _categoriesService
                .GetCategoriesByDepartmentAsync(departmentId);

            var result = categories.Select(c => new
            {
                id = c.Id,
                name = c.Name
            });

            return Json(result);
        }

    }
}
