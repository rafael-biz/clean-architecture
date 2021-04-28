using CleanArchitecture.Entities;
using CleanArchitecture.Services.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.DataAccess.Users
{
    public class UserRegistrationTokenRepository : IUserRegistrationTokenRepository
    {
        private readonly Dictionary<string, UserRegistrationToken> data = new Dictionary<string, UserRegistrationToken>();

        public void Create(UserRegistrationToken token)
        {
            if (data.ContainsKey(token.Token))
            {
                data[token.Token] = token;
            }
            else
            {
                data.Add(token.Token, token);
            }
        }

        public UserRegistrationToken GetByToken(string token)
        {
            if (data.ContainsKey(token))
            {
                return data[token];
            }
            else
            {
                return null;
            }
        }
    }
}
