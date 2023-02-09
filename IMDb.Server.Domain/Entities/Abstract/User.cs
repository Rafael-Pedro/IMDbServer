namespace IMDb.Server.Domain.Entities.Abstract;

public abstract class User : Entity
{
    public bool IsActive { get; set; } = true;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
}
