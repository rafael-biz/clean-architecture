using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Services.Users.UserConfirmation
{
    [Serializable]
    public sealed class UserRegistrationNotFoundException : Exception
    {
        public UserRegistrationNotFoundException(string token) : base($@"The registration token '{ token }' was not found.")
        {
        }
    }
}