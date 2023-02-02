using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class EmailOptions
{
    public const string OptionsKey = "Email";

    [Required]
    public string Host { get; init; } = null!;
}
