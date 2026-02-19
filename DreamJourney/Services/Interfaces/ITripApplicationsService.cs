using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using DreamJourney.ViewModels.TripApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace DreamJourney.Services.Interfaces
{
    public interface ITripApplicationsService
    {
        Task ApplyAsync(int tripId, int userId, int peopleCount);
        Task AcceptAsync(int appId, int organizerId);
        Task RejectAsync(int appId, int organizerId);
        Task CancelAsync(int appId, int userId);
        Task FinalizeAsync(int appId, int userId);
        Task<List<TripApplicationDetailsViewModel>> GetApplicationsForTripAsync(int tripId);
        Task<List<TripApplicationDetailsViewModel>> GetMyApplicationsAsync(int userId);

    }
}
