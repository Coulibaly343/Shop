using System.Threading.Tasks;
using Shop.Core.Domains;
using Shop.Core.Domains.Abstract;

namespace Shop.Infrastructure.Repositories.Interfaces {
    public interface IAdminRepository {
        Task AddAsync (Admin admin);
        Task<Admin> GetByIdAsync (int id, bool isTracking = true);
        Task<Admin> GetByEmailAsync (string email, bool isTracking = true);
        Task<Admin> GetWithAddressAsync (int id, bool isTracking = true);
        Task UpdateAsync (Admin admin);
        Task DeleteAsync (Admin admin);
    }
}