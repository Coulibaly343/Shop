using System.Threading.Tasks;
using Shop.Core.Domains;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class AdminService : IAdminService {
        private readonly IAdminRepository _adminRepository;

        public AdminService (IAdminRepository adminRepository) {
            _adminRepository = adminRepository;
        }

        public async Task<bool> ExistByIdAsync (int id) =>
            await _adminRepository.GetByIdAsync (id, false) != null;

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _adminRepository.GetByEmailAsync (email, false) != null;

        public async Task UpdateAsync (int id, string name, string surname) {
            var admin = await _adminRepository.GetByIdAsync (id);
            admin.Update (name, surname);
            await _adminRepository.UpdateAsync (admin);
        }

        public async Task DeleteAsync (int id) {
            var admin = await _adminRepository.GetByIdAsync (id);
            admin.Delete ();
            await _adminRepository.DeleteAsync (admin);
        }
    }
}