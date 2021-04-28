using System;
using System.Collections.Generic;
using CleanArchitecture.Entities;

namespace CleanArchitecture.Services.Users.UserActivation
{
    public class UserActivationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationTokenRepository _userRegistrationTokenRepository;

        public UserActivationService(IUserRepository userRepository, IUserRegistrationTokenRepository userRegistrationTokenRepository)
        {
            _userRepository = userRepository;
            _userRegistrationTokenRepository = userRegistrationTokenRepository;
        }

        /// <summary>
        /// Confirms an user registration token.
        /// </summary>
        /// <param name="token">
        /// A registration token.
        /// </param>
        /// <exception cref="UserRegistrationTokenNotFoundException">
        /// The activation token was not found.
        /// </exception>
        /// <exception cref="UserRegistrationTokenInactiveException">
        /// The activation token has already been used.
        /// </exception>
        public void ConfirmRegistration(string token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            UserRegistrationToken registration = _userRegistrationTokenRepository.GetByToken(token);

            if (registration == null)
                throw new UserRegistrationTokenNotFoundException(token);

            if (!registration.Active)
                throw new UserRegistrationTokenInactiveException(token);

            registration.Active = false;

            var user = _userRepository.GetById(registration.UserId);

            user.Active = true;

            _userRepository.Save(user);
        }
    }
}