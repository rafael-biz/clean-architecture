using System;

namespace CleanArchitecture.Services.Users.UserConfirmation
{
    [Serializable]
    public sealed class UserRegistrationConfirmedException : Exception
    {
        public UserRegistrationConfirmedException(string token) : base($@"The registration token '{ token }' has already been confirmed.")
        {
        }
    }
}