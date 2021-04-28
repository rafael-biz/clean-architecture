using System;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    public sealed class UserEmailInvalidException: Exception
    {
        public UserEmailInvalidException(string email) : base($@"The user's email '{ email }' is invalid.")
        {
        }
    }
}
