using System;
using System.Threading.Tasks;
using Shop.Core.Domains.Abstract;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class AccountService : IAccountService {
        private readonly IAccountRepository _accountRepository;

        public AccountService (IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }


        public async Task<bool> ExistsByIdAsync (int id) =>
            await _accountRepository.GetByIdAsync (id, false) != null;

        public async Task<bool> ExistsByEmailAsync (string email) =>
            await _accountRepository.GetByEmailAsync (email, false) != null;

        public async Task UpdateAsync (int id, string name, string surname, string iCE, DateTime dateOfBirth, string weight, string height, string school, string sizeOfClothe, string sizeOfShoes, string flatNumber, string streetNumber, string street, string city, string zipCode) {
            throw new NotImplementedException ();
        }
    }
}