using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{


    /// <summary>
    /// Defines the contract for user-related operations within the service layer.
    /// </summary>
    public interface IWidgetService
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
    public class WidgetService : IWidgetService
    {
        private readonly IWidgetRepository _widgetRepository;

        public WidgetService(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<Widget> GetWidget(int id)
        {
            return await _widgetRepository.GetWidget(id);
        }

        public async Task<Widget> SaveWidget(Widget widget)
        {
            return await _widgetRepository.SaveWidget(widget);
        }

        public async Task<bool> DeleteWidgetAsync(Widget widget)
        {
            return await _widgetRepository.DeleteWidgetAsync(widget);
        }

        public async Task<IEnumerable<Widget>> GetAllWidgetsAsync()
        {
            return await _widgetRepository.GetAllWidgetsAsync();
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> SaveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
