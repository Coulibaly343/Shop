using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Core.Domains.Abstract;
using Shop.Infrastructure.Dto.Account;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class AccountService : IAccountService {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService (IAccountRepository accountRepository, IMapper mapper) {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> GetAllDtoAsync () {
            var accounts = await _accountRepository.GetAllAsync ();
            return _mapper.Map<IEnumerable<AccountDto>> (accounts);
        }

        public async Task<bool> ExistsByIdAsync (int id) =>
            await _accountRepository.GetByIdAsync (id, false) != null;

        public async Task<bool> ExistsByEmailAsync (string email) =>
            await _accountRepository.GetByEmailAsync (email, false) != null;

        public async Task UpdateAsync (int id, string name, string surname, string flatNumber, string streetNumber, string street, string city, string zipCode) {
            throw new NotImplementedException ();
        }
    }
}