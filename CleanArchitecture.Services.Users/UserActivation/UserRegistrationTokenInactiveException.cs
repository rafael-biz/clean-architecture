using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Services.Users.UserActivation
{
    [Serializable]
    public sealed class UserRegistrationTokenInactiveException : Exception
    {
        public UserRegistrationTokenInactiveException(string token) : base($@"The activation token '{ token }' has already been used.")
        {
        }
    }
}