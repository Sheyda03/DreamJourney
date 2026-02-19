using AutoMapper;
using DreamJourney.Data;
using DreamJourney.Filters;
using DreamJourney.Services;
using DreamJourney.Services.Interfaces;
using DreamJourney.Services.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Database
builder.Services.AddDbContext<DreamJourneyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITripsService, TripsService>();
builder.Services.AddScoped<ITripApplicationsService, TripApplicationsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// Filters
builder.Services.AddScoped<TripCreatorFilter>();
builder.Services.AddScoped<LoginRequiredFilter>();
builder.Services.AddScoped<TravellerFilter>();

var app = builder.Build();

// ====== Middleware ======

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
