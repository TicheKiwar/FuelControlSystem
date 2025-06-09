using Common.Middlewares;
using Common.Models;
using Common.Shared.Constants;
using Common.Shared.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Reflection;

namespace Common.Shared
{
    public static class Config
    {
        public static WebApplication CreateWebApplication(
            Action<IServiceCollection, IConfiguration>? configureInfrastructure = null,
            Action<WebApplicationBuilder>? additionalConfiguration = null,
            params Assembly[] applicationAssemblies)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddJsonFile("./Api/appsettings.json", optional: false, reloadOnChange: true);

            builder.Services.AddControllers();
            builder.Services.AddGrpc();
            ConfigureSwagger(builder);
            ConfigureLayers(builder, configureInfrastructure, applicationAssemblies);
            ConfigureAuthentication(builder);
            ConfigureAuthorization(builder);

            additionalConfiguration?.Invoke(builder);

            return builder.Build();
        }


        public static void RunWebAplication(WebApplication app, Action<WebApplication>? additionalConfiguration = null)
        {
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

            additionalConfiguration?.Invoke(app);

            app.Run();
        }

        private static void ConfigureLayers(
            WebApplicationBuilder builder,
            Action<IServiceCollection, IConfiguration>? configureInfrastructure = null,
            Assembly[] applicationAssemblies = null!)
        {
            if (applicationAssemblies != null && applicationAssemblies.Length > 0)
            {
                builder.Services.AddApplication(applicationAssemblies);
            }
            else
            {
                builder.Services.AddApplication(Assembly.GetEntryAssembly()!);
            }

            configureInfrastructure?.Invoke(builder.Services, builder.Configuration);
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

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
