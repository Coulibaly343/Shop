using System;
using System.Threading.Tasks;
using Shop.Core.Domains;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class AuthService : IAuthService {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;

        public AuthService (IAdminRepository adminRepository,
            IUserRepository userRepository,
            IAccountService accountService) {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _accountService = accountService;
        }

        public async Task RegisterAdminAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode) {
            if (await _accountService.ExistsByEmailAsync (email.ToLowerInvariant ()))
                throw new Exception ("Email is already taken.");
            var admin = new Admin (name, surname, email, password);
            admin.AddAddress (new Address (flatNumber, streetNumber, street, city, zipCode));
            await _adminRepository.AddAsync (admin);
        }

        public async Task RegisterUserAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode) {
            if (await _accountService.ExistsByEmailAsync (email.ToLowerInvariant ()))
                throw new Exception ("Email is already taken.");
            var user = new User (name, surname, email, password);
            user.AddAddress (new Address (flatNumber, streetNumber, street, city, zipCode));
            await _userRepository.AddAsync (user);
        }
    }
}