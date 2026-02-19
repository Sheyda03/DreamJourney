using DreamJourney.Filters;
using DreamJourney.Services;
using DreamJourney.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DreamJourney.Controllers
{
    [LoginRequiredFilter]
    public class TripApplicationsController : Controller
    {
        private readonly ITripApplicationsService _appsService;
        private readonly IUserContextService _userContext;

        public TripApplicationsController(
            ITripApplicationsService appsService,
            IUserContextService userContext)
        {
            _appsService = appsService;
            _userContext = userContext;
        }

        // APPLY
        public async Task<IActionResult> Apply(int tripId, int peopleCount)
        {
            await _appsService.ApplyAsync(tripId, _userContext.UserId, peopleCount);
            return RedirectToAction("Details", "Trips", new { id = tripId });
        }

        // CANCEL
        public async Task<IActionResult> Cancel(int id)
        {
            await _appsService.CancelAsync(id, _userContext.UserId);
            return RedirectToAction("List", "Trips");
        }

        // ACCEPT
        public async Task<IActionResult> Accept(int id)
        {
            await _appsService.AcceptAsync(id, _userContext.UserId);
            return RedirectToAction("Details", "Trips", new { id = id });
        }

        // REJECT
        public async Task<IActionResult> Reject(int id)
        {
            await _appsService.RejectAsync(id, _userContext.UserId);
            return RedirectToAction("Details", "Trips", new { id = id });
        }

        [LoginRequiredFilter]
        public async Task<IActionResult> MyTrips()
        {
            var apps = await _appsService
                .GetMyApplicationsAsync(_userContext.UserId);

            return View(apps);
        }

        [LoginRequiredFilter]
        public async Task<IActionResult> Finalize(int id)
        {
            await _appsService.FinalizeAsync(id, _userContext.UserId);
            return RedirectToAction("MyTrips");
        }
        [HttpPost]
        public async Task<IActionResult> ApplyFromDetails(int tripId, int peopleCount)
        {
            if (peopleCount <= 0)
            {
                TempData["Error"] = "Моля въведи валиден брой хора.";
                return RedirectToAction("Details", "Trips", new { id = tripId });
            }

            try
            {
                await _appsService.ApplyAsync(
                    tripId,
                    _userContext.UserId,
                    peopleCount
                );
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Details", "Trips", new { id = tripId });
        }

    }
}
