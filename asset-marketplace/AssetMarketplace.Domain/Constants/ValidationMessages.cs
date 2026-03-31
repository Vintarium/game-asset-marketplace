namespace AssetMarketplace.Domain.Constants
{
    public static class ValidationMessages
    {
        public const string EmailEmpty = "Email cannot be empty";
        public const string EmailInvalid = "Incorrect format";
        public const string RoleInvalid = "A non-existent role was specified. Valid values: 0, 1, 2, 4";
        public const string PasswordEmpty = "Password cannot be empty";
        public const string PasswordInvalid = "The minimum password length is";
    }
}
