using System;
using System.Threading.Tasks;
using Shop.Core.Domains;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class AuthService : IAuthService {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AuthService (IAdminRepository adminRepository, IUserRepository userRepository) {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        public async Task RegisterAdminAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode) {
            var admin = new Admin (name, surname, email, password);
            admin.AddAddress (new Address (flatNumber, streetNumber, street, city, zipCode));
            await _adminRepository.AddAsync (admin);
        }

        public async Task RegisterUserAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode) {
            var user = new User (name, surname, email, password);
            user.AddAddress (new Address (flatNumber, streetNumber, street, city, zipCode));
            await _userRepository.AddAsync (user);
        }
    }
}