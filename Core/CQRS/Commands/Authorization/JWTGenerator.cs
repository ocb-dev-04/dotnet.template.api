using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using Core.Models;
using Core.Entities;

namespace Core.CQRS;

/// <summary>
/// Class to generate jwt
/// </summary>
internal static class JWTGenerator
{
    /// <summary>
    /// Generate token with user based
    /// </summary>
    /// <param name="user"></param>
    /// <param name="jwtSettings"></param>
    /// <returns></returns>
    public static JwtModel GenerateJwt(
        this Credential user,
        JwtSettings jwtSettings)
    {
        // set some basic claims
        HashSet<Claim> claims = new HashSet<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

#if DEBUG
        DateTime expirationToken = DateTime.Now.AddYears(1);
#else
        DateTime expirationToken = DateTime.Now.AddDays(1);
#endif

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: claims,
                expires: expirationToken,
                signingCredentials: credentials
            );

        JwtModel jwtModel = new JwtModel();
        jwtModel.Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        jwtModel.TokenExpiryTime = expirationToken;

        return jwtModel;
    }
}
