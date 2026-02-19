using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamJourney.Data;
using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using DreamJourney.ViewModels.Trip;
using Microsoft.EntityFrameworkCore;

namespace DreamJourney.Services
{
    public class TripsService : ITripsService
    {
        private readonly DreamJourneyDbContext _context;
        private readonly IMapper _mapper;

        public TripsService(DreamJourneyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateTripAsync(TripEditViewModel model, int userId)
        {
            var trip = _mapper.Map<Trip>(model);
            trip.UserId = userId;

            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync(); 

            var featureIds = new List<int>();

            if (model.SelectedFeatureIds != null)
            {
                featureIds.AddRange(model.SelectedFeatureIds);
            }

            if (!string.IsNullOrWhiteSpace(model.SingleSelectedFeatureIds))
            {
                var singleIds = model.SingleSelectedFeatureIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse);

                featureIds.AddRange(singleIds);
            }

            foreach (var fid in featureIds.Distinct())
            {
                await _context.TripFeatures.AddAsync(new TripFeature
                {
                    TripId = trip.Id,
                    FeatureId = fid
                });
            }

            await _context.SaveChangesAsync();

            return trip.Id;
        }
        public async Task UpdateTripAsync(TripEditViewModel model, int userId)
        {
            var trip = await _context.Trips
                .Include(t => t.TripFeatures)
                .FirstOrDefaultAsync(t => t.Id == model.Id && t.UserId == userId);

            if (trip == null)
                throw new Exception("Trip not found or not owned.");

            _mapper.Map(model, trip);

            _context.TripFeatures.RemoveRange(trip.TripFeatures);

            var featureIds = new List<int>();

            if (model.SelectedFeatureIds != null)
            {
                featureIds.AddRange(model.SelectedFeatureIds);
            }

            if (!string.IsNullOrWhiteSpace(model.SingleSelectedFeatureIds))
            {
                var singleIds = model.SingleSelectedFeatureIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse);

                featureIds.AddRange(singleIds);
            }

            trip.TripFeatures = featureIds
                .Distinct()
                .Select(fid => new TripFeature
                {
                    TripId = trip.Id,
                    FeatureId = fid
                })
                .ToList();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTripAsync(int tripId, int userId)
        {
            var trip = await _context.Trips
                .Include(t => t.TripApplications)
                .Include(t => t.TripFeatures)
                .FirstOrDefaultAsync(t => t.Id == tripId && t.UserId == userId);

            if (trip == null)
                throw new Exception("Trip not found or not owned by user.");

            _context.TripApplications.RemoveRange(trip.TripApplications);
            _context.TripFeatures.RemoveRange(trip.TripFeatures);
            _context.Trips.Remove(trip);

            await _context.SaveChangesAsync();
        }

        public async Task<TripSearchViewModel> SearchAsync(TripSearchViewModel model)
        {
            var query = _context.Trips
                .Include(t => t.Category)
                .Include(t => t.SubCategory)
                .Include(t => t.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                query = query.Where(t =>
                    t.Title.Contains(model.SearchTerm) ||
                    t.Destinations.Contains(model.SearchTerm));
            }

            if (model.CategoryId.HasValue)
                query = query.Where(t => t.CategoryId == model.CategoryId);

            if (model.SubCategoryId.HasValue)
                query = query.Where(t => t.SubCategoryId == model.SubCategoryId);

            if (model.DepartmentId.HasValue)
                query = query.Where(t => t.DepartmentId == model.DepartmentId);

            if (model.MinPrice.HasValue)
                query = query.Where(t => t.Price >= model.MinPrice);

            if (model.MaxPrice.HasValue)
                query = query.Where(t => t.Price <= model.MaxPrice);

            model.Results = await query
                .Select(t => new TripListViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    ImageUrl = t.ImageUrl,
                    Destinations = t.Destinations,
                    Days = t.Days,
                    Price = t.Price,
                    DepartmentName = t.Department.Name,
                    CategoryName = t.Category.Name,
                    SubCategoryName = t.SubCategory.Name
                })
                .ToListAsync();

            return model;
        }
        public async Task<List<TripListViewModel>> GetSimilarTripsAsync(int tripId)
        {
            var currentTrip = await _context.Trips
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == tripId);

            if (currentTrip == null)
                return new List<TripListViewModel>();

            return await _context.Trips
                .Where(t => t.Id != tripId 
                    && ((t.DepartmentId == currentTrip.DepartmentId 
                    && t.CategoryId == currentTrip.CategoryId) 
                    || t.Destinations == currentTrip.Destinations))
                .OrderByDescending(t => t.Date)
                .Take(3)
                .Select(t => new TripListViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    ImageUrl = t.ImageUrl,
                    Destinations = t.Destinations,
                    Days = t.Days,
                    Price = t.Price,
                    DepartmentName = t.Department.Name,
                    CategoryName = t.Category.Name,
                    SubCategoryName = t.SubCategory.Name
                })
                .ToListAsync();
        }

        public async Task<List<TripListViewModel>> GetAllTripsAsync()
            => await _context.Trips
                .Include(t => t.User)
                .ProjectTo<TripListViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<TripDetailsViewModel> GetTripDetailsAsync(int id)
            => await _context.Trips
                .Include(t => t.User)
                .Include(t => t.SubCategory)
                    .ThenInclude(sc => sc.Category)
                        .ThenInclude(c => c.Department)
                .Include(t => t.TripFeatures)
                    .ThenInclude(tf => tf.Feature)
                .ProjectTo<TripDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == id);


        public async Task<TripEditViewModel> GetTripForEditAsync(int id, int userId)
        {
            return await _context.Trips
                .Where(t => t.Id == id && t.UserId == userId)
                .Include(t => t.TripFeatures)
                .ProjectTo<TripEditViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        public async Task<List<TripListViewModel>> GetTripsByCreatorAsync(int userId)
        {
            return await _context.Trips
                .Where(t => t.UserId == userId)
                .ProjectTo<TripListViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

    }
}
