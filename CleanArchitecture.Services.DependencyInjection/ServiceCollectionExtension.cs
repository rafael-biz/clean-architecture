using CleanArchitecture.Services.Users.UserConfirmation;
using CleanArchitecture.Services.Users.UserRegistration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchitecture
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCleanArchitectureServices(this IServiceCollection services)
        {
            services.AddTransient<UserRegistrationService>();

            services.AddTransient<UserConfirmationService>();

            return services;
        }
    }
}
