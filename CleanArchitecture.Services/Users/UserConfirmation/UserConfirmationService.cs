using System;
using CleanArchitecture.DataAccess.Users;
using CleanArchitecture.Entities;

namespace CleanArchitecture.Services.Users.UserConfirmation
{
    public class UserConfirmationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationTokenRepository _userRegistrationTokenRepository;

        public UserConfirmationService(IUserRepository userRepository, IUserRegistrationTokenRepository userRegistrationTokenRepository)
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
        /// <exception cref="UserRegistrationNotFoundException">
        /// The registration token was not found.
        /// </exception>
        /// <exception cref="UserRegistrationConfirmedException">
        /// The registration token has already been confirmed.
        /// </exception>
        public void ConfirmRegistration(string token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            UserRegistrationToken registration = _userRegistrationTokenRepository.GetByToken(token);

            if (registration == null)
                throw new UserRegistrationNotFoundException(token);

            if (registration.Confirmed)
                throw new UserRegistrationConfirmedException(token);

            registration.Confirmed = true;

            var user = _userRepository.GetById(registration.UserId);

            user.Active = true;

            _userRepository.Save(user);
        }

        /// <summary>
        /// Returns true if the user has confirmed the registration. Otherwise, returns false.
        /// </summary>
        /// <param name="token">
        /// A registration token.
        /// </param>
        /// <exception cref="UserRegistrationNotFoundException">
        /// The registration token was not found.
        /// </exception>
        public bool IsConfirmed(string token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            UserRegistrationToken registration = _userRegistrationTokenRepository.GetByToken(token);

            if (registration == null)
                throw new UserRegistrationNotFoundException(token);

            return registration.Confirmed;
        }
    }
}