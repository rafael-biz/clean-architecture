using System;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    public class UserEmailDuplicatedException : Exception
    {
        public UserEmailDuplicatedException(string email) : base($@"The user's email '{ email }' is duplicated.")
        {
        }
    }
}
