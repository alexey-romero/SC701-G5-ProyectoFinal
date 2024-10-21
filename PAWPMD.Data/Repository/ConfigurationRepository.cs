using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Interface that defines operations for managing Configurations.
    /// </summary>
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Asynchronously deletes a Configuration from the repository.
        /// </summary>
        /// <param name="configuration">The Configuration entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteConfigurationAsync(Configuration configuration);

        /// <summary>
        /// Asynchronously retrieves all Configurations from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of Configurations.</returns>
        Task<IEnumerable<Configuration>> GetAllConfigurationsAsync();

        /// <summary>
        /// Asynchronously retrieves a specific Configuration by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the Configuration to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Configuration entity, or null if the Configuration is not found.</returns>
        Task<Configuration> GetConfiguration(int id);

        /// <summary>
        /// Asynchronously saves a Configuration entity in the repository. If the Configuration already exists, it is updated; otherwise, it is created.
        /// </summary>
        /// <param name="configuration">The Configuration entity to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated Configuration entity.</returns>
        Task<Configuration> SaveConfiguration(Configuration configuration);
    }

    /// <summary>
    /// Repository implementation for managing Configurations, inheriting from the base repository.
    /// </summary>
    public class ConfigurationRepository : RepositoryBase<Configuration>, IConfigurationRepository
    {
        /// <summary>
        /// Asynchronously saves a Configuration entity in the repository. If the Configuration already exists (based on the ConfigurationId), it is updated; otherwise, a new Configuration is created.
        /// </summary>
        /// <param name="Configuration">The Configuration entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated Configuration entity.</returns>
        public async Task<Configuration> SaveConfiguration(Configuration configuration)
        {
            var exists = configuration.ConfigurationId != null && configuration.ConfigurationId > 0;
            if (exists)
                await UpdateAsync(configuration);
            else
                await CreateAsync(configuration);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.ConfigurationId == configuration.ConfigurationId);
        }

        /// <summary>
        /// Asynchronously deletes a Configuration entity from the repository.
        /// </summary>
        /// <param name="configuration">The Configuration entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteConfigurationAsync(Configuration configuration)
        {
            return await DeleteAsync(configuration);
        }

        /// <summary>
        /// Asynchronously retrieves a specific Configuration by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the Configuration to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Configuration entity, or null if the Configuration is not found.</returns>
        public async Task<Configuration> GetConfiguration(int id)
        {
            var configurations = await ReadAsync();
            return configurations.SingleOrDefault(u => u.ConfigurationId == id);
        }

        /// <summary>
        /// Asynchronously retrieves all Configurations from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of Configurations.</returns>
        public async Task<IEnumerable<Configuration>> GetAllConfigurationsAsync()
        {
            return await ReadAsync();
        }
    }
}
