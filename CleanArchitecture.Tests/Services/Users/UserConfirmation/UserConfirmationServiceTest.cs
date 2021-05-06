using CleanArchitecture.Services.Users.UserRegistration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CleanArchitecture.Services.Users.UserConfirmation
{
    [TestClass]
    public class UserConfirmationServiceTest
    {
        private UserRegistrationService userRegistrationService;

        private UserConfirmationService userConfirmationService;

        [TestInitialize]
        public void TestInitialize()
        {
            var services = new ServiceCollection();

            services.AddCleanArchitectureServices();
            services.AddCleanArchitectureMocks();

            var provider = services.BuildServiceProvider();

            userRegistrationService = provider.GetRequiredService<UserRegistrationService>();
            userConfirmationService = provider.GetRequiredService<UserConfirmationService>();
        }

        [TestMethod]
        [Description("Must confirm a user's registration.")]
        public void TestConfirmRegistration()
        {
            var dto = new UserRegistrationDTO()
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "123456",
            };

            var token = userRegistrationService.RegisterUser(dto);

            Assert.IsFalse(userConfirmationService.IsConfirmed(token.Token));

            userConfirmationService.ConfirmRegistration(token.Token);

            Assert.IsTrue(userConfirmationService.IsConfirmed(token.Token));
        }

        [TestMethod]
        [Description("Must prevent verification more than once.")]
        public void TestDuplicatedConfirmation()
        {
            var dto = new UserRegistrationDTO()
            {
                Name = "Mary",
                Email = "mary@gmail.com",
                Password = "123456",
            };

            var token = userRegistrationService.RegisterUser(dto);

            userConfirmationService.ConfirmRegistration(token.Token);

            Assert.ThrowsException<UserRegistrationConfirmedException>(() =>
            {
                userConfirmationService.ConfirmRegistration(token.Token);
            });
        }

        [TestMethod]
        [Description("Deve impedir a utilização de um token inválido.")]
        public void TestInvalidConfirmation()
        {
            Assert.ThrowsException<UserRegistrationNotFoundException>(() =>
            {
                userConfirmationService.ConfirmRegistration("abc1234");
            });
        }
    }
}
