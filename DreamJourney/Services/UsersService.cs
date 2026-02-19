using DreamJourney.Data;
using DreamJourney.Data.Models;
using DreamJourney.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamJourney.Services
{
    public class UsersService : IUsersService
    {
        private readonly DreamJourneyDbContext _context;

        public UsersService(DreamJourneyDbContext context)
        {
            _context = context;
        }

        public Task<User> GetByIdAsync(int id)
            => _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public Task<User> GetByUsernameAsync(string username)
            => _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public Task<User> GetByUsernameAndPasswordAsync(string username, string password)
            => _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        public async Task RegisterAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
