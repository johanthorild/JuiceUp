﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Providers;

using Domain.Entities;

using Infrastructure.Options;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Providers;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtOptions _jwtSettings;
    private readonly AddressOptions _addressesSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenProvider(
        IOptions<JwtOptions> jwtOptions,
        IOptions<AddressOptions> addressesOptions,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtSettings = jwtOptions.Value;
        _addressesSettings = addressesOptions.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(User user)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        // Build claims
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        //claims.AddRange(user.UserRoles.Select(x => new Claim(ClaimTypes.Role, x)));

        // Add audience if not already existing in the provided claims
        var shouldAddAudienceClaim =
            string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);


        var securityToken = new JwtSecurityToken(
            issuer: _addressesSettings.SiteUrl,
            audience: shouldAddAudienceClaim ? _addressesSettings.SiteUrl : null,
            signingCredentials: credentials,
            claims: claims,
            notBefore: _dateTimeProvider.UtcNow,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationTimeInMinutes));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}