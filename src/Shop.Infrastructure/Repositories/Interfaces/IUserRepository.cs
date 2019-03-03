using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Core.Domains;

namespace Shop.Infrastructure.Repositories.Interfaces {
    public interface IUserRepository {
        Task AddAsync (User user);
        Task<IEnumerable<User>> GetAllAsync (bool isTracking = true);
        Task<User> GetByIdAsync (int id, bool isTracking = true);
        Task<User> GetByEmailAsync (string email, bool isTracking = true);
        Task<User> GetWithAddressAsync (int id, bool isTracking = true);
        Task UpdateAsync (User user);
        Task DeleteAsync (User user);
    }
}