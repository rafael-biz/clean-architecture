using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Services.Users.UserActivation
{
    [Serializable]
    public sealed class UserRegistrationTokenNotFoundException : Exception
    {
        public UserRegistrationTokenNotFoundException(string token) : base($@"The activation token '{ token }' was not found.")
        {
        }
    }
}