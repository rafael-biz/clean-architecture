using CleanArchitecture.Entities;

namespace CleanArchitecture.DataAccess.Users
{
    public interface IUserRegistrationTokenRepository
    {
        /// <summary>
        /// Creates an user registration token.
        /// </summary>
        /// <param name="token">
        /// A registration token.
        /// </param>
        public void Create(UserRegistrationToken token);

        /// <summary>
        /// Returns an user registration token or null.
        /// </summary>
        /// <param name="token">
        /// Token.
        /// </param>
        public UserRegistrationToken GetByToken(string token);
    }
}
