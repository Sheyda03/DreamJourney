using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamJourney.Data;
using DreamJourney.Data.Models;
using DreamJourney.Data.Models.Enums;
using DreamJourney.Services.Interfaces;
using DreamJourney.ViewModels.TripApplication;
using Microsoft.EntityFrameworkCore;

namespace DreamJourney.Services
{
    public class TripApplicationsService : ITripApplicationsService
    {
        private readonly DreamJourneyDbContext _context;
        private readonly IMapper _mapper;

        public TripApplicationsService(DreamJourneyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ApplyAsync(int tripId, int userId, int peopleCount)
        {
            if (peopleCount <= 0)
                throw new Exception("Invalid people count.");

            var exists = await _context.TripApplications
                            .FirstOrDefaultAsync(ta =>
                                ta.TripId == tripId &&
                                ta.UserId == userId &&
                                (ta.Status == ApplicationStatus.Pending ||
                                 ta.Status == ApplicationStatus.Accepted));

            if (exists != null)
                throw new Exception("Already applied.");

            var trip = await _context.Trips
                .Include(t => t.TripApplications)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null)
                throw new Exception("Trip does not exist.");

            int usedSeats = trip.TripApplications
                .Where(a => a.Status == ApplicationStatus.Accepted ||
                            a.Status == ApplicationStatus.Completed)
                .Sum(a => a.PeopleCount);

            if (usedSeats + peopleCount > trip.Seats)
                throw new Exception("Not enough available seats.");

            var application = new TripApplication
            {
                TripId = tripId,
                UserId = userId,
                PeopleCount = peopleCount,
                Status = ApplicationStatus.Pending
            };

            await _context.TripApplications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptAsync(int appId, int organizerId)
        {
            var app = await _context.TripApplications
                .Include(a => a.Trip)
                .Include(a => a.Trip.TripApplications)
                .FirstOrDefaultAsync(a => a.Id == appId);

            if (app == null)
                throw new Exception("Application not found.");

            if (app.Trip.UserId != organizerId)
                throw new Exception("Forbidden.");

            int usedSeats = app.Trip.TripApplications
                .Where(a => a.Status == ApplicationStatus.Accepted ||
                            a.Status == ApplicationStatus.Completed)
                .Sum(a => a.PeopleCount);

            if (usedSeats + app.PeopleCount > app.Trip.Seats)
                throw new Exception("Not enough seats.");

            app.Status = ApplicationStatus.Accepted;
            await _context.SaveChangesAsync();
        }

        public async Task RejectAsync(int appId, int organizerId)
        {
            var app = await _context.TripApplications
                .Include(a => a.Trip)
                .FirstOrDefaultAsync(a => a.Id == appId);

            if (app == null)
                throw new Exception("Not found.");

            if (app.Trip.UserId != organizerId)
                throw new Exception("Forbidden.");

            app.Status = ApplicationStatus.Rejected;
            await _context.SaveChangesAsync();
        }

        public async Task CancelAsync(int appId, int userId)
        {
            var app = await _context.TripApplications
                .FirstOrDefaultAsync(a => a.Id == appId);

            if (app == null || app.UserId != userId)
                throw new Exception("Not allowed.");

            _context.TripApplications.Remove(app);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TripApplicationDetailsViewModel>> GetApplicationsForTripAsync(int tripId)
        {
            return await _context.TripApplications
                .Where(a => a.TripId == tripId)
                .Include(a => a.Trip)
                .Include(a => a.User)
                .OrderByDescending(a => a.Id)
                .ProjectTo<TripApplicationDetailsViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<TripApplicationDetailsViewModel>> GetMyApplicationsAsync(int userId)
        {
            return await _context.TripApplications
                .Where(a => a.UserId == userId)
                .Include(a => a.Trip)
                .Include(a => a.User)
                .OrderByDescending(a => a.Id)
                .ProjectTo<TripApplicationDetailsViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task FinalizeAsync(int appId, int userId)
        {
            var app = await _context.TripApplications
                .Include(a => a.Trip)
                .FirstOrDefaultAsync(a => a.Id == appId);

            if (app == null || app.UserId != userId)
                throw new Exception("Not allowed.");

            if (app.Status != ApplicationStatus.Accepted)
                throw new Exception("Application not approved.");

            app.Status = ApplicationStatus.Completed;
            await _context.SaveChangesAsync();
        }
    }
}
