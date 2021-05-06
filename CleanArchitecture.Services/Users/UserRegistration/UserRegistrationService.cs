using CleanArchitecture.Controllers;
using CleanArchitecture.DataAccess.Users;
using CleanArchitecture.Entities;
using CleanArchitecture.Services.Users.UserConfirmation;
using System;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    public sealed class UserRegistrationService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRegistrationEmail userRegistrationEmail;
        private readonly UserConfirmationService userConfirmationService;

        public UserRegistrationService(IUserRepository userRepository, IUserRegistrationEmail userRegistrationEmail, UserConfirmationService userConfirmationService)
        {
            this.userRepository = userRepository;
            this.userRegistrationEmail = userRegistrationEmail;
            this.userConfirmationService = userConfirmationService;
        }

        /// <summary>
        /// Registers a new user and sends him a confirmation email.
        /// </summary>
        /// <returns>
        /// A registration token.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Dto must not be null.
        /// </exception>
        /// <exception cref="UserEmailDuplicatedException">
        /// The user's email is already registered.
        /// </exception>
        /// <exception cref="UserEmailRequiredException">
        /// The user's email is required.
        /// </exception>
        /// <exception cref="UserEmailInvalidException">
        /// The user's email is invalid.
        /// </exception>
        public UserRegistrationToken RegisterUser(UserRegistrationDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Email == null)
                throw new UserEmailRequiredException(dto.Email);

            if (!dto.Email.Contains("@"))
                throw new UserEmailInvalidException(dto.Email);

            if (IsRegistred(dto.Email))
                throw new UserEmailDuplicatedException(dto.Email);

            var user = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password
            };

            userRepository.Save(user);

            var token = userConfirmationService.CreateRegistrationToken(user);

            userRegistrationEmail.SendUserRegistrationEmail(user.Email, user.Name, token.Token);

            return token;
        }

        /// <summary>
        /// Returns true if the specified e-mail is registred.
        /// </summary>
        /// <param name="email">
        /// User's email.
        /// </param>
        public bool IsRegistred(string email)
        {
            return userRepository.Contains(email);
        }
    }
}