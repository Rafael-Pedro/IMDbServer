namespace IMDb.Server.Application.Services.Cryptography;
public interface ICryptographyService
{
    byte[] Hash(string plainText, byte[] salt);
    byte[] CreateSalt();
    bool Compare(byte[] originalHash, byte[] originalSalt, string plainText);
}
