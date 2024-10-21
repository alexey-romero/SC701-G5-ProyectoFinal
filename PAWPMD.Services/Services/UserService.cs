using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Defines the contract for user-related operations within the service layer.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously deletes a user from the system.
        /// </summary>
        /// <param name="user">The user entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteUser(User user);

        /// <summary>
        /// Asynchronously retrieves all users from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of users.</returns>
        Task<IEnumerable<User>> GetAllUsers();

        /// <summary>
        /// Asynchronously retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity if found, otherwise null.</returns>
        Task<User> GetUser(int id);


        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>A <see cref="User"/> object if found; otherwise, null.</returns>
        Task<User> GetUserByUsernameAsync(string username);


        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>A <see cref="User"/> object if found; otherwise, null.</returns>
        Task<User> GetUserByEmailAsync(string email);


        /// <summary>
        /// Asynchronously saves a user entity. If the user already exists, it updates the user; otherwise, it creates a new user.
        /// </summary>
        /// <param name="user">The user entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated user entity.</returns>
        Task<User> SaveUser(User user);
    }

    /// <summary>
    /// Implements the user service which handles user-related operations, interacting with the repository layer.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository instance to interact with the data layer.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity if found, otherwise null.</returns>
        public async Task<User> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>A <see cref="User"/> object if found; otherwise, null.</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>A <see cref="User"/> object if found; otherwise, null.</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        /// <summary>
        /// Asynchronously retrieves all users from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of users.</returns>
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        /// <summary>
        /// Asynchronously saves a user entity. If the user already exists, it updates the user; otherwise, it creates a new user.
        /// </summary>
        /// <param name="user">The user entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated user entity.</returns>
        public async Task<User> SaveUser(User user)
        {
            return await _userRepository.SaveUser(user);
        }

        /// <summary>
        /// Asynchronously deletes a user from the system.
        /// </summary>
        /// <param name="user">The user entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteUser(User user)
        {
            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
