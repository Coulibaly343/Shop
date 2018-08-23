using System;
using System.Threading.Tasks;
using Shop.Core.Domains;

namespace Shop.Infrastructure.Services.Interfaces {
    public interface IAdminService {
        Task<bool> ExistByIdAsync (int id);
        Task<bool> ExistByEmailAsync (string email);
        Task UpdateAsync (int id, string name, string surname);
        Task DeleteAsync (int id);
    }
}