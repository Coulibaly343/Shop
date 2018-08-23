using System;
using System.Threading.Tasks;
using Shop.Core.Domains;
using Shop.Core.Domains.Abstract;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class AuthService : IAuthService {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;

        public AuthService (IAdminRepository adminRepository,
            IUserRepository userRepository,
            IAccountService accountService,
            IAccountRepository accountRepository) {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        public async Task<Account> LoginAsync (string email, string password) {
            var account = await _userRepository.GetByEmailAsync (email, true);
            if (account == null || !account.Activated || account.Deleted)
                return null;
            if (!VerifyPasswordHash (password, account.PasswordHash, account.PasswordSalt))
                return null;
            return account;
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
                var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
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