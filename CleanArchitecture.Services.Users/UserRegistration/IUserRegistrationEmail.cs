using System;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    public interface IUserRegistrationEmail
    {
        /// <summary>
        /// Sends a confirmation email with a confirmation token.
        /// </summary>
        /// <param name="email">
        /// User's e-mail.
        /// </param>
        /// <param name="name">
        /// User's name.
        /// </param>
        /// <param name="token">
        /// A confirmation token.
        /// </param>
        void SendUserRegistrationEmail(string email, string name, string token);
    }
}
