namespace Application.Providers;
public interface IHttpContextUserProvider
{
    Guid Id { get; }
    string Email { get; }
    public bool IsAdmin { get; }
}