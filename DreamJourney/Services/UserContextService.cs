using DreamJourney.Services.Interfaces;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session;

    public bool IsLoggedIn
        => Session?.GetInt32("UserId").HasValue == true;

    public int UserId
        => Session?.GetInt32("UserId") ?? 0;

    public string Username
        => Session?.GetString("Username");

    public bool IsTripCreator
        => Session?.GetString("Role") == "TripCreator";
}
