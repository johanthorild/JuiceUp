namespace Application.Providers;
public interface IHttpContextUserProvider
{
    // Unique username
    string Email { get; }
}