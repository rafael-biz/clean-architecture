using System;
using CleanArchitecture.DataAccess.Users;
using CleanArchitecture.Entities;

namespace CleanArchitecture.Services.Users.UserConfirmation
{
    public class UserConfirmationService
    {
        private readonly IUserRegistrationTokenRepository userRegistrationTokenRepository;

        public UserConfirmationService(IUserRegistrationTokenRepository userRegistrationTokenRepository)
        {
            this.userRegistrationTokenRepository = userRegistrationTokenRepository;
        }

        /// <summary>
        /// Creates  user registration token.
        /// </summary>
        /// <param name="user">
        /// An user.
        /// </param>
        /// <returns>
        /// Returns a registration token.
        /// </returns>
        public UserRegistrationToken CreateRegistrationToken(User user)
        {
            var token = new UserRegistrationToken()
            {
                Token = Guid.NewGuid().ToString(),
                Confirmed = false,
                UserId = user.UserId
            };

            userRegistrationTokenRepository.Create(token);

            return token;
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

            UserRegistrationToken registration = userRegistrationTokenRepository.GetByToken(token);

            if (registration == null)
                throw new UserRegistrationNotFoundException(token);

            if (registration.Confirmed)
                throw new UserRegistrationConfirmedException(token);

            registration.Confirmed = true;
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

            UserRegistrationToken registration = userRegistrationTokenRepository.GetByToken(token);

            if (registration == null)
                throw new UserRegistrationNotFoundException(token);

            return registration.Confirmed;
        }
    }
}