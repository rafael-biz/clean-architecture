using CleanArchitecture.Services.Users.UserRegistration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Controllers.EmailSender
{
    public sealed class UserRegistrationEmailSender : IUserRegistrationEmail
    {
        public void SendUserRegistrationEmail(string email, string name, string token)
        {
            
        }
    }
}
