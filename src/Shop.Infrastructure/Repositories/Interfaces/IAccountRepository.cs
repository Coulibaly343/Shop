using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Core.Domains.Abstract;

namespace Shop.Infrastructure.Repositories.Interfaces {
    public interface IAccountRepository {
        Task<IEnumerable<Account>> GetAllAsync (bool isTracking = true);
        Task<Account> GetByIdAsync (int id, bool isTracking = true);
        Task<Account> GetByEmailAsync (string email, bool isTracking = true);
        Task<Account> GetWithAddressAsync (int id, bool isTracking = true);
        Task UpdateAsync (Account account);
    }
}