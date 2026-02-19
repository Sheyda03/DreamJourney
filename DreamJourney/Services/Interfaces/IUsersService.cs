using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DreamJourney.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
        Task RegisterAsync(User user);
    }
}
