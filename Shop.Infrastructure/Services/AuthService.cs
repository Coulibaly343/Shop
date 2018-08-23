using System;
using System.Threading.Tasks;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public Task RegisterAdminAsync(string name, string surname, string email, string password,string flatNumber, string streetNumber,
            string street, string city, string zipCode)
        {
            throw new NotImplementedException();
        }
    }
}