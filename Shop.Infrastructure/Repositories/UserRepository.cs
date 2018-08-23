using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domains;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repositories.Interfaces;

namespace Shop.Infrastructure.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ShopContext _context;

        public UserRepository (ShopContext context) {
            _context = context;
        }

        public async Task AddAsync (User user) {
            await _context.Users.AddAsync (user);
            await _context.SaveChangesAsync ();
        }

        public async Task<IEnumerable<User>> GetAllAsync (bool isTracking = true) {
            if (isTracking)
                return await Task.FromResult (_context.Users.AsTracking ().AsEnumerable ());
            return await Task.FromResult (_context.Users.AsNoTracking ().AsEnumerable ());
        }

        public async Task<User> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking)
                return await _context.Users.AsTracking ().SingleOrDefaultAsync (x => x.Id == id);
            return await _context.Users.AsNoTracking ().SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<User> GetByEmailAsync (string email, bool isTracking = true) {
            if (isTracking)
                return await _context.Users.AsTracking ().SingleOrDefaultAsync (x => x.Email == email);
            return await _context.Users.AsNoTracking ().SingleOrDefaultAsync (x => x.Email == email);
        }

        public async Task<User> GetWithAddressAsync (int id, bool isTracking = true) {
            if (isTracking)
                return await _context.Users.AsTracking ().Include (x => x.Address).SingleOrDefaultAsync (x => x.Id == id);
            return await _context.Users.AsNoTracking ().Include (x => x.Address).SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task UpdateAsync (User user) {
            _context.Users.Update (user);
            await _context.SaveChangesAsync ();
        }

        public async Task DeleteAsync (User user) {
            _context.Users.Remove (user);
            await _context.SaveChangesAsync ();
        }
    }
}