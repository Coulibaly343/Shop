using System;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
         Task RegisterAdminAsync (string name, string surname, string email, string password, string iCE, DateTime dateOfBirth, string weight, string height, string school,
            string sizeOfClothe, string sizeOfShoes, string flatNumber, string streetNumber, string street, string city, string zipCode);
    }
}