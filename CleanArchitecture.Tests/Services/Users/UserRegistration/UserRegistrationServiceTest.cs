using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    [TestClass]
    public class UserRegistrationServiceTest
    {
        private UserRegistrationService userRegistrationService;

        [TestInitialize]
        public void TestInitialize()
        {
            var services = new ServiceCollection();

            services.AddCleanArchitectureServices();
            services.AddCleanArchitectureMocks();

            var provider = services.BuildServiceProvider();

            userRegistrationService = provider.GetRequiredService<UserRegistrationService>();
        }

        [TestMethod]
        [Description("It must register an user.")]
        public void TestRegistration()
        {
            var dto = new UserRegistrationDTO()
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "123456",
            };

            Assert.IsFalse(userRegistrationService.IsRegistred(dto.Email));

            userRegistrationService.RegisterUser(dto);

            Assert.IsTrue(userRegistrationService.IsRegistred(dto.Email));
        }

        [TestMethod]
        [Description("It must prevent registering two users with the same email.")]
        public void TestDuplicatedRegistration()
        {
            userRegistrationService.RegisterUser(new UserRegistrationDTO()
            {
                Name = "John",
                Email = "john@gmail.com",
                Password = "123456",
            });

            Assert.ThrowsException<UserEmailDuplicatedException>(() =>
            {
                userRegistrationService.RegisterUser(new UserRegistrationDTO()
                {
                    Name = "Mary",
                    Email = "john@gmail.com",
                    Password = "999999",
                });
            });
        }

        [TestMethod]
        [Description("It must prevent a user from registering without an email.")]
        public void TestEmailRequired()
        {
            Assert.ThrowsException<UserEmailRequiredException>(() =>
            {
                userRegistrationService.RegisterUser(new UserRegistrationDTO()
                {
                    Name = "Mary",
                    Email = null,
                    Password = "999999",
                });
            });
        }

        [TestMethod]
        [Description("It must prevent a user from registering without a valid email.")]
        public void TestEmailInvalid()
        {
            Assert.ThrowsException<UserEmailInvalidException>(() =>
            {
                userRegistrationService.RegisterUser(new UserRegistrationDTO()
                {
                    Name = "John",
                    Email = "aaaaaaa",
                    Password = "999999",
                });
            });
        }
    }
}
