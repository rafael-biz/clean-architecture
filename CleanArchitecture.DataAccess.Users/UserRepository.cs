using CleanArchitecture.Entities;
using CleanArchitecture.Services.Users;
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
                Birthdate = dto.Birthdate,
                Active = dto.Active,
                PhoneNumber = dto.PhoneNumber
            };

            return user;
        }

        public void Save(User user)
        {
            data.Add(user.UserId, new User()
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                Active = user.Active
            });
        }

        public bool Contains(string email)
        {
            return data.Values.Any(u => u.Email == email);
        }
    }
}
