using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Defines the contract for role-related operations in the repository.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Asynchronously deletes a role entity from the repository.
        /// </summary>
        /// <param name="role">The role entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteRoleAsync(Role role);

        /// <summary>
        /// Asynchronously retrieves all roles from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of roles.</returns>
        Task<IEnumerable<Role>> GetAllRoles();

        /// <summary>
        /// Asynchronously retrieves a specific role by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity, or null if the role is not found.</returns>
        Task<Role> GetRoleAsync(int id);

        /// <summary>
        /// Asynchronously saves a role entity in the repository. If the role already exists, it is updated; otherwise, a new role is created.
        /// </summary>
        /// <param name="role">The role entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated role entity.</returns>
        Task<Role> SaveRole(Role role);

        /// <summary>
        /// Asynchronously saves a collection of role entities in the repository. If a role exists, it is updated; otherwise, a new role is created.
        /// </summary>
        /// <param name="roles">The collection of role entities to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> SaveRolesAsync(IEnumerable<Role> roles);


        /// <summary>
        /// Asynchronously retrieves a specific role by name from the repository.
        /// </summary>
        /// <param name="name">The name of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity, or null if the role is not found.</returns>
        Task<Role> GetRoleByNameAsync(string name);
    }

    /// <summary>
    /// Implements the role repository for handling role-related operations, inheriting from the base repository.
    /// </summary>
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        /// <summary>
        /// Asynchronously saves a role entity in the repository. If the role already exists (based on RoleId), it is updated; otherwise, a new role is created.
        /// </summary>
        /// <param name="role">The role entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated role entity.</returns>
        public async Task<Role> SaveRole(Role role)
        {
            var exists = role.RoleId != null && role.RoleId > 0;
            if (exists)
                await UpdateAsync(role);
            else
                await CreateAsync(role);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.RoleId == role.RoleId);
        }

        /// <summary>
        /// Asynchronously saves a collection of role entities in the repository. If a role exists, it is updated; otherwise, a new role is created.
        /// </summary>
        /// <param name="roles">The collection of role entities to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> SaveRolesAsync(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                var exists = await ExistsAsync(role);
                if (exists)
                    await UpdateAsync(role);
                else
                    await CreateAsync(role);
            }
            return true;
        }

        /// <summary>
        /// Asynchronously deletes a role entity from the repository.
        /// </summary>
        /// <param name="role">The role entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteRoleAsync(Role role)
        {
            return await DeleteAsync(role);
        }

        /// <summary>
        /// Asynchronously retrieves a specific role by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity, or null if the role is not found.</returns>
        public async Task<Role> GetRoleAsync(int id)
        {
            var roles = await ReadAsync();
            return roles.SingleOrDefault(x => x.RoleId == id);
        }

        /// <summary>
        /// Asynchronously retrieves a specific role by name from the repository.
        /// </summary>
        /// <param name="name">The name of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role entity, or null if the role is not found.</returns>
        public async Task<Role> GetRoleByNameAsync(string name)
        {
            var roles = await ReadAsync();
            return roles.SingleOrDefault(x => x.Name == name);
        }
        /// <summary>
        /// Asynchronously retrieves all roles from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of roles.</returns>
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await ReadAsync();
        }
    }
}

