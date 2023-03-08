using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Infrastructure.Storage;
using Infrastructure.JWT;
using Application.Grpc;
using Infrastructure.GrpcClient;
using Infrastructure.Exceptions;

namespace Infrastructure
{
    public static class Extensions
    {
        public static T GetProjectOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOption = configuration.GetProjectOptions<JwtSettings>("JWT");
            services.AddScoped<ExceptionMiddleware>();
            services.AddMemoryCache();
            services.AddSingleton(jwtOption);
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IUserRequestStorage, UserRequestStorage>();
            services.AddScoped<IRequestStorage, RequestStorage>();
            services.AddScoped<INotificationGrpcClient, NotificationGrpcClient>();
            services.AddPostgres(configuration);
            return services;
        }
    }
}
