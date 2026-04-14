using System.ComponentModel.DataAnnotations;

namespace AssetMarketplace.Infrastructure.Settings
{
    public class DatabaseOptions
    {
        public const string SectionName = "ConnectionStrings";

        [Required]
        public string DefaultConnection { get; set; } = string.Empty;
    }
}
