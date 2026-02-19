using AutoMapper;
using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using DreamJourney.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public UsersController(IUsersService usersService, IMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }

    // GET
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existingUser = await _usersService.GetByUsernameAsync(model.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Потребителското име вече съществува!");
            return View(model);
        }

        var user = _mapper.Map<User>(model);

        await _usersService.RegisterAsync(user);

        return RedirectToAction("Login");
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _usersService
            .GetByUsernameAndPasswordAsync(model.Username, model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Грешна парола или потребителско име!");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("Role", user.Role ? "TripCreator" : "Traveller");

        return RedirectToAction("Index", "Home");
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
