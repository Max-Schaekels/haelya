using Haelya.Application.DTOs.User;
using Haelya.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Haelya.Shared.Settings;
using Microsoft.Extensions.Options;



namespace Haelya.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        public string GenerateToken(UserDTO user)
        {
            var secret = _jwtSettings.Key;
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            var expiresInMinutes = _jwtSettings.ExpiresInMinutes;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            if (string.IsNullOrWhiteSpace(user.Role))
            {

                throw new InvalidOperationException("Le rôle de l'utilisateur est requis pour générer le token.");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? "Unknown"),
                new Claim(ClaimTypes.Role, user.Role.ToLowerInvariant()) // Normalisation
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
