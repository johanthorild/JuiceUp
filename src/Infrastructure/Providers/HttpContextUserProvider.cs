using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Application.Providers;

using Microsoft.AspNetCore.Http;

namespace Infrastructure.Providers;

public class HttpContextUserProvider : IHttpContextUserProvider
{
    private readonly ClaimsPrincipal? _user;

    public HttpContextUserProvider(IHttpContextAccessor httpContext)
    {
        _user = httpContext.HttpContext?.User;
    }

    public Guid Id
    {
        get
        {
            if (_user is null)
            {
                throw new InvalidOperationException("Cannot get username when no user is logged in. This indicates a bug in the backend.");
            }

            return Guid.Parse(_user.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sid).FirstOrDefault()?.Value ?? string.Empty);
        }
    }

    public string Email
    {
        get
        {
            if (_user is null)
            {
                throw new InvalidOperationException("Cannot get username when no user is logged in. This indicates a bug in the backend.");
            }

            return _user.Identity!.Name!;
        }
    }

    public bool IsAdmin
    {
        get
        {
            if (_user is null)
            {
                throw new InvalidOperationException("Cannot get username when no user is logged in. This indicates a bug in the backend.");
            }

            return _user.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "Admin");
        }
    }
}
