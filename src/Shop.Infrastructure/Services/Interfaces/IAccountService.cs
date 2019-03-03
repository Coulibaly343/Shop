using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Core.Domains.Abstract;
using Shop.Infrastructure.Dto.Account;

namespace Shop.Infrastructure.Services.Interfaces {
    public interface IAccountService {
        Task<IEnumerable<AccountDto>> GetAllDtoAsync ();
        Task<bool> ExistsByIdAsync (int id);
        Task<bool> ExistsByEmailAsync (string email);
        Task UpdateAsync (int id, string name, string surname, string flatNumber, string streetNumber, string street, string city, string zipCode);
    }
}