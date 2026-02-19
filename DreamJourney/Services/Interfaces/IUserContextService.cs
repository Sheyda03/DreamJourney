using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJourney.Services.Interfaces
{
    public interface IUserContextService
    {
        int UserId { get; }
        string Username { get; }
        bool IsTripCreator { get; }
        bool IsLoggedIn { get; }
    }
}
