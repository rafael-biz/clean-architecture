using CleanArchitecture.Controllers;
using CleanArchitecture.Controllers.EmailSender;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchitecture
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCleanArchitectureControllers(this IServiceCollection services)
        {
            services.AddTransient<IUserRegistrationEmail, EmailSenderController>();

            return services;
        }
    }
}
