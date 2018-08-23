using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domains;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repositories.Interfaces;

namespace Shop.Infrastructure.Repositories {
    public class AdminRepository : IAdminRepository {
        private readonly ShopContext _context;

        public AdminRepository (ShopContext context) {
            _context = context;
        }
        public async Task AddAsync (Admin admin) {
            await _context.Admins.AddAsync (admin);
            await _context.SaveChangesAsync ();
        }

        public async Task<Admin> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking)
                return await _context.Admins.AsTracking ().SingleOrDefaultAsync (x => x.Id == id);
            return await _context.Admins.AsNoTracking ().SingleOrDefaultAsync (x => x.Id == id);

        }

        public async Task<Admin> GetByEmailAsync (string email, bool isTracking = true) {
            if (isTracking)
                return await _context.Admins.AsTracking ().SingleOrDefaultAsync (x => x.Email == email);
            return await _context.Admins.AsNoTracking ().SingleOrDefaultAsync (x => x.Email == email);
        }

        public async Task<Admin> GetWithAddressAsync (int id, bool isTracking = true) {
            if (isTracking)
                return await _context.Admins.AsTracking ().Include (x => x.Address).SingleOrDefaultAsync (x => x.Id == id);
            return await _context.Admins.AsNoTracking ().Include (x => x.Address).SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task UpdateAsync (Admin admin) {
            _context.Admins.Update (admin);
            await _context.SaveChangesAsync ();
        }

        public async Task DeleteAsync (Admin admin) {
            _context.Admins.Remove (admin);
            await _context.SaveChangesAsync ();
        }
    }
}