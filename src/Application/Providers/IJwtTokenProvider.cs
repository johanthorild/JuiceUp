using Domain.Entities;

namespace Application.Providers;

public interface IJwtTokenProvider
{
    string GenerateToken(User user);
}

