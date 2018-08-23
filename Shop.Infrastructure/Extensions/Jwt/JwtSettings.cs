namespace Shop.Infrastructure.Extensions.Jwt {
    public class JwtSettings : IJwtSettings {
        public string Key { get; set; }
        public int ExpiryDays { get; set; }
    }
}