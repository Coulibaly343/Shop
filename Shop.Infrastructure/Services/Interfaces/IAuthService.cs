using System;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services.Interfaces {
    public interface IAuthService {
        Task RegisterAdminAsync (string name, string surname, string email, string password, string flatNumber, string streetNumber,
            string street, string city, string zipCode);
    }
}