using System.Security.Cryptography;
using System.Text;

namespace IMDb.Server.Application.Services.Cryptography;
public class CryptographyService : ICryptographyService
{
    public bool Compare(byte[] originalHash, byte[] originalSalt, string plainText)
    {
        var plainTextHash = Hash(plainText, originalSalt);
        return originalHash.SequenceEqual(plainTextHash);
    }

    public byte[] CreateSalt()
    {
        var rng = RandomNumberGenerator.Create();
        var buff = new byte[10];
        rng.GetBytes(buff);
        return buff;
    }

    public byte[] Hash(string plainText, byte[] salt)
        => SHA256.HashData(Encoding.UTF8.GetBytes(plainText).Concat(salt).ToArray());
}
