using AssetMarketplace.Domain.Abstractions;

namespace AssetMarketplace.Domain.Constants;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found.");

    public static readonly Error EmailNotUnique = new(
        "User.EmailNotUnique",
        "The provided email is already in use.");

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid email or password.");
}
