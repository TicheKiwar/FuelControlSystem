using AuthService.AuthService.Api.Middleware;
using AuthService.AuthService.Api.Settings;
using AuthService.AuthService.Application.Common.Constants;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthService.AuthService.Infrastructure;
using AuthService.AuthService.Api.Middleware.AuthService.AuthService.Api.Middleware;
using System.Text.Json;

namespace AuthService.AuthService.Api.Config
{
    public static class FuelWebApplication
    {
        public static WebApplication CreateWebApplication(
            Action<WebApplicationBuilder>? additionalConfiguration = null)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddJsonFile("./AuthService.Api/appsettings.json", optional: false, reloadOnChange: true);

            // Configuración base
            builder.Services.AddControllers();
            ConfigureSwagger(builder);
            ConfigureLayers(builder);
            ConfigureAuthentication(builder);
            ConfigureAuthorization(builder);

            // Configuración adicional personalizada
            additionalConfiguration?.Invoke(builder);

            return builder.Build();
        }

        public static void RunWebApplication(
            WebApplication app,
            Action<WebApplication>? additionalConfiguration = null)
        {
            // Configuración base del pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Configuración adicional del pipeline
            additionalConfiguration?.Invoke(app);

            app.Run();
        }

        private static void ConfigureLayers(WebApplicationBuilder builder)
        {
            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
            Console.WriteLine($"Key: {jwtSettings?.Key}, Issuer: {jwtSettings?.Issuer}, Audience: {jwtSettings?.Audience}");

            if (jwtSettings == null || string.IsNullOrWhiteSpace(jwtSettings.Key))
            {
                throw new InvalidOperationException("JWT configuration is missing or invalid");
            }

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize(new { message = "No autorizado" })
                        );
                    },
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void ConfigureAuthorization(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(RoleConstants.Admin, policy => policy.RequireRole(RoleConstants.Admin));
                options.AddPolicy(RoleConstants.Operador, policy => policy.RequireRole(RoleConstants.Operador));
                options.AddPolicy(RoleConstants.Supervisor, policy => policy.RequireRole(RoleConstants.Supervisor));
            });
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });
        }
    }
}
