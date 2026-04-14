using AssetMarketplace.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AssetMarketplace.Infrastructure.Security;

internal class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        return Convert.ToHexString(bytes);
    }
}
