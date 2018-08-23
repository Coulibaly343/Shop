namespace Shop.Infrastructure.Extensions.Jwt {
    public interface IJwtSettings {
        string Key { get; set; }
        int ExpiryDays { get; set; }
    }
}