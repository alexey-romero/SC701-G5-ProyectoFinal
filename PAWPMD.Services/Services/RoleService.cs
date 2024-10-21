using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Defines the contract for role-related operations within the service layer.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Asynchronously deletes a role from the system.
        /// </summary>
        /// <param name="role">The role entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteRole(Role role);

        /// <summary>
        /// Asynchronously retrieves all roles from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of roles.</returns>
        Task<IEnumerable<Role>> GetAllRoles();

        /// <summary>
        /// Asynchronously retrieves a role by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity if found, otherwise null.</returns>
        Task<Role> GetRole(int id);
        /// <summary>
        /// Asynchronously saves a role entity. If the role already exists, it updates the role; otherwise, it creates a new role.
        /// </summary>
        /// <param name="role">The role entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated role entity.</returns>
        Task<Role> SaveRole(Role role);

        /// <summary>
        /// Asynchronously retrieves a role by their name.
        /// </summary>
        /// <param name="name">The name of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity if found, otherwise null.</returns>
        Task<Role> GetRoleByNameAsync(string name);
    }

    /// <summary>
    /// Implements the role service which handles role-related operations, interacting with the repository layer.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository instance to interact with the data layer.</param>
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a role by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity if found, otherwise null.</returns>
        public async Task<Role> GetRole(int id)
        {
            return await _roleRepository.GetRoleAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves a role by their name.
        /// </summary>
        /// <param name="name">The name of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity if found, otherwise null.</returns>
        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _roleRepository.GetRoleByNameAsync(name);
        }

        /// <summary>
        /// Asynchronously retrieves all roles from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of roles.</returns>
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        /// <summary>
        /// Asynchronously saves a role entity. If the role already exists, it updates the role; otherwise, it creates a new role.
        /// </summary>
        /// <param name="role">The role entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated role entity.</returns>
        public async Task<Role> SaveRole(Role role)
        {
            return await _roleRepository.SaveRole(role);
        }

        /// <summary>
        /// Asynchronously deletes a role from the system.
        /// </summary>
        /// <param name="role">The role entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteRole(Role role)
        {
            return await _roleRepository.DeleteRoleAsync(role);
        }
    }
}
