using CleanArchitecture.Entities;

namespace CleanArchitecture.Services.Users
{
    public interface IUserRepository
    {
        /// <summary>
        /// Returns an user.
        /// </summary>
        /// <param name="userId">
        /// The user's id.
        /// </param>
        /// <exception cref="UserNotFoundException">
        /// The user was not found.
        /// </exception>
        public User GetById(string userId);

        /// <summary>
        /// Creates or updates an user.
        /// </summary>
        /// <param name="user">
        /// </param>
        public void Save(User user);

        /// <summary>
        /// Returns true if a user exists with the specified email.
        /// </summary>
        public bool Contains(string email);
    }
}
