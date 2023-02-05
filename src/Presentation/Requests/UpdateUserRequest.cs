namespace Presentation.Requests;
internal sealed record UpdateUserRequest(
    Guid Id,
    string Email,
    string Firstname,
    string Lastname,
    string PasswordBase64);
