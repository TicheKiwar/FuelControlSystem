using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.AuthService.Api.Middleware
{

    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    namespace AuthService.AuthService.Api.Middleware
    {
        public class JwtMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly IConfiguration _configuration;

            public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
            {
                _next = next;
                _configuration = configuration;
            }

            public async Task Invoke(HttpContext context)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                    AttachUserToContext(context, token);

                await _next(context);
            }

            private void AttachUserToContext(HttpContext context, string token)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var claims = jwtToken.Claims.ToList();

                    // Agregar identidad al contexto
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error validating token: {ex.Message}");
                    // No hacemos nada si falla la validación
                }
            }
        }
    }
}
