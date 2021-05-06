using System;

namespace CleanArchitecture.Controllers.EmailSender
{
    public sealed class EmailSenderController : IUserRegistrationEmail
    {
        public void SendUserRegistrationEmail(string email, string name, string token)
        {
            
        }
    }
}
