using System;
using CleanArchitecture.Controllers.EmailSender;
using CleanArchitecture.DataAccess.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    [TestClass]
    public class UserRegistrationServiceTest
    {
        private UserRegistrationService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            var userRepository = new UserRepository();
            var userRegistrationTokenRepository = new UserRegistrationTokenRepository();
            var userRegistrationEmailSender = new UserRegistrationEmailSender();

            _service = new UserRegistrationService(userRepository, userRegistrationTokenRepository, userRegistrationEmailSender);
        }

        [TestMethod]
        [Description("Deve permitir o registro de usuários.")]
        public void TestRegistration()
        {
            var dto = new UserRegistrationDTO()
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "123456",
            };

            var token = _service.RegisterUser(dto);

            Assert.IsNotNull(token, "O token de registro do usuário é inválido.");
        }

        [TestMethod]
        [Description("Deve impedir o registro de dois usuários com o mesmo e-mail.")]
        public void TestDuplicatedRegistration()
        {
            _service.RegisterUser(new UserRegistrationDTO()
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "123456",
            });

            Assert.ThrowsException<UserEmailDuplicatedException>(() =>
            {
                _service.RegisterUser(new UserRegistrationDTO()
                {
                    Name = "Biltrano",
                    Email = "fulano@gmail.com",
                    Password = "999999",
                });
            });
        }

        [TestMethod]
        [Description("Deve impedir o registro de um usuário sem um e-mail.")]
        public void TestEmailRequired()
        {
            Assert.ThrowsException<UserEmailRequiredException>(() =>
            {
                _service.RegisterUser(new UserRegistrationDTO()
                {
                    Name = "Fulano",
                    Email = null,
                    Password = "999999",
                });
            });
        }

        [TestMethod]
        [Description("Deve impedir o registro de um usuário sem um e-mail válido.")]
        public void TestEmailInvalid()
        {
            Assert.ThrowsException<UserEmailInvalidException>(() =>
            {
                _service.RegisterUser(new UserRegistrationDTO()
                {
                    Name = "Fulano",
                    Email = "aaaaaaa",
                    Password = "999999",
                });
            });
        }
    }
}
