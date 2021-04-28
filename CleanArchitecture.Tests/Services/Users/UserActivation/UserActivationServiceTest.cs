using CleanArchitecture.Controllers.EmailSender;
using CleanArchitecture.DataAccess.Users;
using CleanArchitecture.Services.Users.UserRegistration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Services.Users.UserActivation
{
    [TestClass]
    public class UserActivationServiceTest
    {
        private UserRegistrationService _userRegistrationService;
        private UserActivationService _userActivationService;

        [TestInitialize]
        public void TestInitialize()
        {
            var userRepository = new UserRepository();
            var userRegistrationTokenRepository = new UserRegistrationTokenRepository();
            var userRegistrationEmailSender = new UserRegistrationEmailSender();

            _userRegistrationService = new UserRegistrationService(userRepository, userRegistrationTokenRepository, userRegistrationEmailSender);
            _userActivationService = new UserActivationService(userRepository, userRegistrationTokenRepository);
        }

        [TestMethod]
        [Description("Deve permitir a ativação de um usuário.")]
        public void TestConfirmation()
        {
            var dto = new UserRegistrationDTO()
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "123456",
            };

            var token = _userRegistrationService.RegisterUser(dto);

            _userActivationService.ConfirmRegistration(token.Token);

            // TODO:
            // Deve impedir o login na aplicação antes de realizar a ativação.
            // Deve permitir o login na aplicação após realizar a ativação.
        }

        [TestMethod]
        [Description("Deve impedir a utilização de um token de ativação duas vezes.")]
        public void TestDuplicatedConfirmation()
        {
            var dto = new UserRegistrationDTO()
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "123456",
            };

            var token = _userRegistrationService.RegisterUser(dto);

            _userActivationService.ConfirmRegistration(token.Token);

            Assert.ThrowsException<UserRegistrationTokenInactiveException>(() =>
            {
                _userActivationService.ConfirmRegistration(token.Token);
            });
        }

        [TestMethod]
        [Description("Deve impedir a utilização de um token inválido.")]
        public void TestInvalidConfirmation()
        {
            Assert.ThrowsException<UserRegistrationTokenNotFoundException>(() =>
            {
                _userActivationService.ConfirmRegistration("abc1234");
            });
        }
    }
}
