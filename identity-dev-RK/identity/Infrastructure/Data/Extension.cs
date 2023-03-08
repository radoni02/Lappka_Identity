using Application.Services;
using Core.Domain.Models;
using Core.Domain.Repositories;
using Infrastructure.JWT;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class Extension
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<ITokenRepository, IdentityTokenRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddDbContext<ApplicationDbContext>(options
                => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false) ////////////IdenittyRole<>Guid
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<ApplicationDbContext>();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>         ////
            opt.TokenLifespan = TimeSpan.FromHours(1));
            return services;

        }
    }
}
