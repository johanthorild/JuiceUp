using Application.Dtos;

using Domain.Entities;

namespace Application.Providers;

public interface IJwtTokenProvider
{
    Task<LoginResult> GenerateToken(User user);
}

