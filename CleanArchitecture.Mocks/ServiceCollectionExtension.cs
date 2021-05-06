using CleanArchitecture.Controllers;
using CleanArchitecture.DataAccess.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCleanArchitectureMocks(this IServiceCollection services)
        {
            services.AddSingleton<IUserRegistrationTokenRepository, UserRegistrationTokenRepository>();

            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IUserRegistrationEmail, EmailSenderControllerMock>();

            return services;
        }
    }
}
