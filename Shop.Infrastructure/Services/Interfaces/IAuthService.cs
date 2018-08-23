using System;
using System.Threading.Tasks;
using Shop.Core.Domains.Abstract;

namespace Shop.Infrastructure.Services.Interfaces {
    public interface IAuthService {
        Task<Account> LoginAsync (string email, string password);
        Task RegisterAdminAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode);
        Task RegisterUserAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode);
    }
}