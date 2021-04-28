using System;

namespace CleanArchitecture.Entities
{
    public class User
    {
        /// <summary>
        /// User's identification code.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User's e-mail.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// True if the user confirms the registration token.
        /// </summary>
        public bool Active { get; set; }
    }
}
