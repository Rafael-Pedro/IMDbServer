namespace IMDb.Server.Domain.Entities.Abstract;

public abstract class Account : Entity
{
    public bool IsActive { get; set; } = true;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordHashSalt { get; set; } = Array.Empty<byte>();
}
