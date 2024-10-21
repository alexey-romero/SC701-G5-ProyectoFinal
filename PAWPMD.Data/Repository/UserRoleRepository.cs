using PAWPMD.Models;


namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Defines the contract for UserRole-related operations in the repository.
    /// </summary>
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Asynchronously deletes a UserRole entity from the repository.
        /// </summary>
        /// <param name="UserRole">The UserRole entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteUserRoleAsync(UserRole userRole);

        /// <summary>
        /// Asynchronously retrieves all UserRoles from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of UserRoles.</returns>
        Task<IEnumerable<UserRole>> GetAllUserRoles();

        /// <summary>
        /// Asynchronously retrieves a specific UserRole by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the UserRole to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UserRole entity, or null if the UserRole is not found.</returns>
        Task<UserRole> GetUserRoleAsync(int id);


        /// <summary>
        /// Retrieves the roles associated with a specific user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of roles associated with the user.</returns
        Task<IEnumerable<UserRole>> GetUserRoleByUserIdAsync(int userId);

        /// <summary>
        /// Asynchronously saves a UserRole entity in the repository. If the UserRole already exists, it is updated; otherwise, a new UserRole is created.
        /// </summary>
        /// <param name="UserRole">The UserRole entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated UserRole entity.</returns>
        Task<UserRole> SaveUserRole(UserRole userRole);

        /// <summary>
        /// Asynchronously saves a collection of UserRole entities in the repository. If a UserRole exists, it is updated; otherwise, a new UserRole is created.
        /// </summary>
        /// <param name="UserRoles">The collection of UserRole entities to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> SaveUserRolesAsync(IEnumerable<UserRole> userRoles);
    }

    /// <summary>
    /// Implements the UserRole repository for handling UserRole-related operations, inheriting from the base repository.
    /// </summary>
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        /// <summary>
        /// Asynchronously saves a UserRole entity in the repository. If the UserRole already exists (based on UserRoleId), it is updated; otherwise, a new UserRole is created.
        /// </summary>
        /// <param name="UserRole">The UserRole entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated UserRole entity.</returns>
        public async Task<UserRole> SaveUserRole(UserRole userRole)
        {
            var exists = userRole.Id != null && userRole.Id > 0;
            if (exists)
                await UpdateAsync(userRole);
            else
                await CreateAsync(userRole);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.Id == userRole.Id);
        }

        /// <summary>
        /// Asynchronously saves a collection of UserRole entities in the repository. If a UserRole exists, it is updated; otherwise, a new UserRole is created.
        /// </summary>
        /// <param name="UserRoles">The collection of UserRole entities to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> SaveUserRolesAsync(IEnumerable<UserRole> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                var exists = await ExistsAsync(userRole);
                if (exists)
                    await UpdateAsync(userRole);
                else
                    await CreateAsync(userRole);
            }
            return true;
        }

        /// <summary>
        /// Asynchronously deletes a UserRole entity from the repository.
        /// </summary>
        /// <param name="UserRole">The UserRole entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteUserRoleAsync(UserRole userRole)
        {
            return await DeleteAsync(userRole);
        }

        /// <summary>
        /// Asynchronously retrieves a specific UserRole by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the UserRole to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UserRole entity, or null if the UserRole is not found.</returns>
        public async Task<UserRole> GetUserRoleAsync(int id)
        {
            var userRoles = await ReadAsync();
            return userRoles.SingleOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// Retrieves the roles associated with a specific user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of roles associated with the user.</returns>
        public async Task<IEnumerable<UserRole>> GetUserRoleByUserIdAsync(int userId)
        {
            var userRoles = await ReadAsync();
            return userRoles.Where(x => x.UserId == userId);
        }


        /// <summary>
        /// Asynchronously retrieves all UserRoles from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of UserRoles.</returns>
        public async Task<IEnumerable<UserRole>> GetAllUserRoles()
        {
            return await ReadAsync();
        }
    }
}

