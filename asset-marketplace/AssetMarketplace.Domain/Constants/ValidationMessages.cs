namespace AssetMarketplace.Domain.Constants
{
    public static class ValidationMessages
    {
        public static string EmptyValue(string propertyName)
        {
            return $"{propertyName}: cannot be empty";
        }

        public static string InvalidValue(string propertyName)
        {
            return $"{propertyName}: invalid value";
        }

        public static string MinimalLengthValue(string propertyName)
        {
            return $"{propertyName}: minimum length: {ValidationConstants.MinPasswordLength}";
        }

        public static string InvalidRole(string enumValue)
        {
            return $"{enumValue}: A non-existent role was specified. Valid values: 0, 1, 2, 4";
        }

    }
}
