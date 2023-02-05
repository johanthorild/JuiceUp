namespace Presentation.Requests;
internal sealed record LoginRequest(
    string Email,
    string PasswordBase64);