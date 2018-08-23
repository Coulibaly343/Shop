using AutoMapper;
using Shop.Core.Domains;
using Shop.Core.Domains.Abstract;
using Shop.Infrastructure.Dto.Account;
using Shop.Infrastructure.Dto.User;

namespace Shop.Infrastructure.AutoMapper {
    public class AutoMapperConfig {
        public static IMapper Initialize () => new MapperConfiguration (cfg => {
            cfg.CreateMap<User, UserDto> ();
            cfg.CreateMap<Account, AccountDto> ();
        }).CreateMapper ();
    }
}