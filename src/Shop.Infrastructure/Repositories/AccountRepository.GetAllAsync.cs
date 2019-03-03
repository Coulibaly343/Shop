using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domains.Abstract;

namespace Shop.Infrastructure.Repositories
{
    public partial class AccountRepository
    {
        public async Task<IEnumerable<Account>> GetAllAsync(bool isTracking = true)
        {
            if (isTracking)
                return await Task.FromResult(_context.Accounts.AsTracking().AsEnumerable());
            return await Task.FromResult(_context.Accounts.AsNoTracking().AsEnumerable());
        }
    }
}