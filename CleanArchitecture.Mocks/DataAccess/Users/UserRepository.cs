using CleanArchitecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.DataAccess.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> data = new Dictionary<string, User>();

        public User GetById(string userId)
        {
            User dto;
            if (!data.TryGetValue(userId, out dto))
            {
                throw new UserNotFoundException(userId);
            }

            var user = new User()
            {
                UserId = dto.UserId,
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password
            };

            return user;
        }

        public void Save(User user)
        {
            var copy = new User()
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };

            if (data.ContainsKey(user.UserId))
                data[user.UserId] = copy;
            else
                data.Add(user.UserId, copy);
        }

        public bool Contains(string email)
        {
            return data.Values.Any(u => u.Email == email);
        }
    }
}
