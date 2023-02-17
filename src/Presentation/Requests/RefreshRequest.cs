namespace Presentation.Requests;
internal sealed record RefreshRequest(
    string AccessToken,
    string RefreshToken);