using AssetMarketplace.Application.Interfaces;

namespace AssetMarketplace.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
