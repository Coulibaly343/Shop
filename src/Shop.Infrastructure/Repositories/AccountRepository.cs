using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domains.Abstract;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repositories.Interfaces;

namespace Shop.Infrastructure.Repositories {
    public partial class AccountRepository : IAccountRepository {
        private readonly ShopContext _context;

        public AccountRepository (ShopContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync(bool isTracking = true)
        {
            if (isTracking)
                return await Task.FromResult(_context.Accounts.AsTracking().AsEnumerable());
            return await Task.FromResult(_context.Accounts.AsNoTracking().AsEnumerable());
        }
        
        public async Task<Account> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking)
                return await _context.Accounts.AsTracking ().SingleOrDefaultAsync (x => x.Id == id);
            return await _context.Accounts.AsNoTracking ().SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<Account> GetByEmailAsync (string email, bool isTracking = true) {
            if (isTracking)
                return await _context.Accounts.AsTracking ().SingleOrDefaultAsync (x => x.Email == email);
            return await _context.Accounts.AsNoTracking ().SingleOrDefaultAsync (x => x.Email == email);
        }

        public async Task<Account> GetWithAddressAsync (int id, bool isTracking = true) {
            if (isTracking)
                return await _context.Accounts.AsTracking ().Include (x => x.Address).SingleOrDefaultAsync (x => x.Id == id);
            return await _context.Accounts.AsNoTracking ().Include (x => x.Address).SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task UpdateAsync (Account account) {
            _context.Accounts.Update (account);
            await _context.SaveChangesAsync ();
        }
    }
}