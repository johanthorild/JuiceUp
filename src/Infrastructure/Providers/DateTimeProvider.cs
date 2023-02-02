using Application.Providers;

namespace Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Fixed => new(2023, 01, 01);
}
