using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Core.Domains;
using Shop.Infrastructure.Dto.User;

namespace Shop.Infrastructure.Services.Interfaces {
    public interface IUserService {
        Task<IEnumerable<UserDto>> GetAllAsync ();
        Task<bool> ExistByIdAsync (int id);
        Task<bool> ExistByEmailAsync (string email);
        Task UpdateAsync (int id, string name, string surname, string flatNumber, string streetNumber,
            string street, string city, string zipCode);
        Task DeleteAsync (int id);
    }
}