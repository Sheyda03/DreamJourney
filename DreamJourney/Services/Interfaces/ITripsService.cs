using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using DreamJourney.ViewModels.Trip;
using System;

namespace DreamJourney.Services.Interfaces
{
    public interface ITripsService
    {
        Task<int> CreateTripAsync(TripEditViewModel model, int userId);
        Task UpdateTripAsync(TripEditViewModel model, int userId);
        Task DeleteTripAsync(int id, int userId);
        Task<TripSearchViewModel> SearchAsync(TripSearchViewModel model);
        Task<List<TripListViewModel>> GetSimilarTripsAsync(int tripId);
        Task<List<TripListViewModel>> GetAllTripsAsync();
        Task<TripDetailsViewModel> GetTripDetailsAsync(int id);
        Task<TripEditViewModel> GetTripForEditAsync(int id, int userId);
        Task<List<TripListViewModel>> GetTripsByCreatorAsync(int userId);
    }

}
