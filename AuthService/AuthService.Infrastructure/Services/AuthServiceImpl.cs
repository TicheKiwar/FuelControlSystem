using AuthService.AuthService.Application.Common.Interfaces;
using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AuthService.AuthService.Api.Settings;

namespace AuthService.AuthService.Infrastructure.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthServiceImpl(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResult> Authenticate(User user, string password)
        {
            if (user == null || !await _userRepository.CheckPasswordAsync(user.Id, password))
            {
                return new AuthResult(false);
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(
                user.Id,
                refreshToken,
                DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays));

            return new AuthResult(
                true,
                token,
                refreshToken,
                DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes));
        }


        public async Task Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.AddAsync(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            claims.AddRange(user.Roles.Select(role =>
                new Claim(ClaimTypes.Role, role.ToString())));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        Task<string> IAuthService.GenerateJwtToken(User user)
        {
            throw new NotImplementedException();
        }

        Task<string> IAuthService.GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> RefreshToken(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
