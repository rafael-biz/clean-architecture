using CleanArchitecture.DataAccess.Users;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchitecture
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCleanArchitectureMongoDB(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserRegistrationTokenRepository, UserRegistrationTokenRepository>();

            return services;
        }
    }
}
