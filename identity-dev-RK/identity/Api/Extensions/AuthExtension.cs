using Application.Services;
using Core.Domain.Models;
using Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            services.AddScoped<IJwtHandler, JwtHandler>();
            //services.AddAuthorization();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var jwtHandler = serviceProvider.GetService<IJwtHandler>();

            services
                .AddAuthorization(o =>
                {
                    o.AddPolicy(Policy.SuperAdmin,policy => policy.RequireRole(Role.SuperAdmin.ToString()));
                    o.AddPolicy(Policy.Admin, policy => policy.RequireRole(Role.Admin.ToString(), Role.SuperAdmin.ToString()));
                    o.AddPolicy(Policy.Shelter, policy => policy.RequireRole(Role.Shelter.ToString(), Role.Admin.ToString(), Role.SuperAdmin.ToString()));
                    o.AddPolicy(Policy.WorkerAndShelter, policy => policy.RequireRole(Role.Worker.ToString(), Role.Shelter.ToString(), Role.Admin.ToString(), Role.SuperAdmin.ToString()));
                    //o.AddPolicy(Policy.onlyLapkaLoginProviderUser, policy => policy.RequireClaim(JwtClaims.LoginProvider, LoginProvider.Lapka.ToString()));
                });

            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                      
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
            opts => {
                opts.TokenValidationParameters = jwtHandler.Parameters;
                opts.SaveToken = true;           
            });

            return services;

        }
    }
}
