using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Defines the contract for UserRole-related operations within the service layer.
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// Asynchronously deletes a UserRole from the system.
        /// </summary>
        /// <param name="UserRole">The UserRole entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteUserRole(UserRole userRole);

        /// <summary>
        /// Asynchronously retrieves all UserRoles from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of UserRoles.</returns>
        Task<IEnumerable<UserRole>> GetAllUserRoles();

        /// <summary>
        /// Retrieves the roles associated with a specific user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose roles are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation, containing a collection of roles associated with the user.</returns>
        Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(int userId);

        /// <summary>
        /// Asynchronously retrieves a UserRole by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the UserRole to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UserRole entity if found, otherwise null.</returns>
        Task<UserRole> GetUserRole(int id);
        /// <summary>
        /// Asynchronously saves a UserRole entity. If the UserRole already exists, it updates the UserRole; otherwise, it creates a new UserRole.
        /// </summary>
        /// <param name="UserRole">The UserRole entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated UserRole entity.</returns>
        Task<UserRole> SaveUserRole(UserRole userRole);
    }

    /// <summary>
    /// Implements the UserRole service which handles UserRole-related operations, interacting with the repository layer.
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleService"/> class.
        /// </summary>
        /// <param name="UserRoleRepository">The UserRole repository instance to interact with the data layer.</param>
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a UserRole by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the UserRole to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UserRole entity if found, otherwise null.</returns>
        public async Task<UserRole> GetUserRole(int id)
        {
            return await _userRoleRepository.GetUserRoleAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all UserRoles from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of UserRoles.</returns>
        public async Task<IEnumerable<UserRole>> GetAllUserRoles()
        {
            return await _userRoleRepository.GetAllUserRoles();
        }
        /// <summary>
        /// Retrieves the roles associated with a specific user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose roles are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation, containing a collection of roles associated with the user.</returns>
        public async Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(int userId)
        {
            return await _userRoleRepository.GetUserRoleByUserIdAsync(userId);
        }

        /// <summary>
        /// Asynchronously saves a UserRole entity. If the UserRole already exists, it updates the UserRole; otherwise, it creates a new UserRole.
        /// </summary>
        /// <param name="userRole">The UserRole entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated UserRole entity.</returns>
        public async Task<UserRole> SaveUserRole(UserRole userRole)
        {
            return await _userRoleRepository.SaveUserRole(userRole);
        }

        /// <summary>
        /// Asynchronously deletes a UserRole from the system.
        /// </summary>
        /// <param name="userRole">The UserRole entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteUserRole(UserRole userRole)
        {
            return await _userRoleRepository.DeleteUserRoleAsync(userRole);
        }
    }
}
