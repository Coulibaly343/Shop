using System;
using System.Threading.Tasks;
using Shop.Core.Domains.Abstract;

namespace Shop.Infrastructure.Services.Interfaces {
    public interface IAccountService {
        Task<bool> ExistsByIdAsync (int id);
        Task<bool> ExistsByEmailAsync (string email);
        Task UpdateAsync (int id, string name, string surname, string iCE, DateTime dateOfBirth, string weight, string height, string school,
            string sizeOfClothe, string sizeOfShoes, string flatNumber, string streetNumber, string street, string city, string zipCode);
    }
}