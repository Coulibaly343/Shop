using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Core.Domains;
using Shop.Infrastructure.Dto.User;
using Shop.Infrastructure.Repositories.Interfaces;
using Shop.Infrastructure.Services.Interfaces;

namespace Shop.Infrastructure.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService (IUserRepository userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllDtoAsync () {
            var users = await _userRepository.GetAllAsync ();
            return _mapper.Map<IEnumerable<UserDto>> (users);
        }

        public async Task<bool> ExistByIdAsync (int id) =>
            await _userRepository.GetByIdAsync (id, false) != null;

        public async Task<bool> ExistByEmailAsync (string email) =>
            await _userRepository.GetByEmailAsync (email, false) != null;

        public async Task UpdateAsync (int id, string name, string surname, string flatNumber, string streetNumber, string street, string city, string zipCode) {
            var user = await _userRepository.GetWithAddressAsync (id);
            user.Update (name, surname);
            user.Address.Update (flatNumber, streetNumber, street, city, zipCode);
            await _userRepository.UpdateAsync (user);
        }

        public async Task DeleteAsync (int id) {
            var user = await _userRepository.GetByIdAsync (id);
            user.Delete ();
            await _userRepository.DeleteAsync (user);
        }
    }
}