namespace AssetMarketplace.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
