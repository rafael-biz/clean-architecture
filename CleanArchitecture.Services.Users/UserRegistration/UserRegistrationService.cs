using CleanArchitecture.Entities;
using System;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    public class UserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationTokenRepository _userRegistrationTokenRepository;
        private readonly IUserRegistrationEmail _userRegistrationEmail;

        public UserRegistrationService(IUserRepository userRepository, IUserRegistrationTokenRepository userRegistrationTokenRepository, IUserRegistrationEmail userRegistrationEmail)
        {
            _userRepository = userRepository;
            _userRegistrationTokenRepository = userRegistrationTokenRepository;
            _userRegistrationEmail = userRegistrationEmail;
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

            if (_userRepository.Contains(dto.Email))
                throw new UserEmailDuplicatedException(dto.Email);

            var user = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password,
                Active = false
            };

            _userRepository.Save(user);

            var token = new UserRegistrationToken()
            {
                Token = Guid.NewGuid().ToString(),
                Active = true,
                UserId = user.UserId
            };

            _userRegistrationTokenRepository.Create(token);

            _userRegistrationEmail.SendUserRegistrationEmail(user.Email, user.Name, token.Token);

            return token;
        }
    }
}