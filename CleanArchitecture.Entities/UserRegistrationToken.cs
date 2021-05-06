using System;

namespace CleanArchitecture.Entities
{
    public sealed class UserRegistrationToken
    {
        /// <summary>
        /// A unique identification token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// User's id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Returns true if the registration token has been confirmed.
        /// </summary>
        public bool Confirmed { get; set; }
    }
}
