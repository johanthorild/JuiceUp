namespace Presentation.Requests;
internal sealed record RegisterRequest(
    string Email,
    string Firstname,
    string Lastname,
    string PasswordBase64);
