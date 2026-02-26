namespace AssetMarketplace.Domain.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);
}
