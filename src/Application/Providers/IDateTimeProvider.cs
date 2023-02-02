namespace Application.Providers;
public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Fixed { get; }
}