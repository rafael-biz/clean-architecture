using System;

namespace CleanArchitecture.Controllers
{
    public sealed class EmailSenderControllerMock : IUserRegistrationEmail
    {
        public void SendUserRegistrationEmail(string email, string name, string token)
        {

        }
    }
}
