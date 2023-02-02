using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class AddressOptions
{
    public const string OptionsKey = "Addresses";

    [Required]
    public string SiteUrl { get; init; } = null!;

    [Required]
    public string SupportEmail { get; init; } = null!;
}
