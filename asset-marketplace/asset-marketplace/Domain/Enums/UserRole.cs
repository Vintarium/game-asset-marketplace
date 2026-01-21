using System;
namespace asset_marketplace.Domain.Enums
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        Buyer = 1,
        Seller = 2,
        Admin = 4
    }
}
