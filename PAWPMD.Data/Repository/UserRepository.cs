using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Interface that defines operations for managing users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Asynchronously deletes a user from the repository.
        /// </summary>
        /// <param name="user">The user entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteUserAsync(User user);

        /// <summary>
        /// Asynchronously retrieves all users from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of users.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Asynchronously retrieves a specific user by Username from the repository.
        /// </summary>
        /// <param name="username">The Username of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity, or null if the user is not found.</returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Asynchronously retrieves a specific user by Email from the repository.
        /// </summary>
        /// <param name="email">The Email of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity, or null if the user is not found.</returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Asynchronously retrieves a specific user by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity, or null if the user is not found.</returns>
        Task<User> GetUser(int id);

        /// <summary>
        /// Asynchronously saves a user entity in the repository. If the user already exists, it is updated; otherwise, it is created.
        /// </summary>
        /// <param name="user">The user entity to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated user entity.</returns>
        Task<User> SaveUser(User user);
    }

    /// <summary>
    /// Repository implementation for managing users, inheriting from the base repository.
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Asynchronously saves a user entity in the repository. If the user already exists (based on the UserId), it is updated; otherwise, a new user is created.
        /// </summary>
        /// <param name="user">The user entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated user entity.</returns>
        public async Task<User> SaveUser(User user)
        {
            var exists = user.UserId != null && user.UserId > 0;
            if (exists)
                await UpdateAsync(user);
            else
                await CreateAsync(user);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.UserId == user.UserId);
        }

        /// <summary>
        /// Asynchronously deletes a user entity from the repository.
        /// </summary>
        /// <param name="user">The user entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteUserAsync(User user)
        {
            return await DeleteAsync(user);
        }

        /// <summary>
        /// Asynchronously retrieves a specific user by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity, or null if the user is not found.</returns>
        public async Task<User> GetUser(int id)
        {
            var users = await ReadAsync();
            return users.SingleOrDefault(u => u.UserId == id);
        }


        /// <summary>
        /// Asynchronously retrieves a specific user by Username from the repository.
        /// </summary>
        /// <param name="username">The Username of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity, or null if the user is not found.</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var users = await ReadAsync();
            return users.FirstOrDefault(u => u.Username == username);
        }

        /// <summary>
        /// Asynchronously retrieves a specific user by Email from the repository.
        /// </summary>
        /// <param name="email">The Email of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity, or null if the user is not found.</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var users = await ReadAsync();
            return users.FirstOrDefault(u => u.Email ==  email);
        }

        /// <summary>
        /// Asynchronously retrieves all users from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of users.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await ReadAsync();
        }
    }
}
