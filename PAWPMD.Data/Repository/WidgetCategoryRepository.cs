using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Interface that defines operations for managing WidgetCategory1s.
    /// </summary>
    public interface IWidgetCategoryRepository
    {
        /// <summary>
        /// Asynchronously deletes a WidgetCategory1 from the repository.
        /// </summary>
        /// <param name="widgetCategory1">The WidgetCategory1 entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteWidgetCategory1Async(WidgetCategory1 widgetCategory1);

        /// <summary>
        /// Asynchronously retrieves all WidgetCategory1s from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of WidgetCategory1s.</returns>
        Task<IEnumerable<WidgetCategory1>> GetAllWidgetCategory1sAsync();

        /// <summary>
        /// Asynchronously retrieves a specific WidgetCategory1 by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the WidgetCategory1 to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the WidgetCategory1 entity, or null if the WidgetCategory1 is not found.</returns>
        Task<WidgetCategory1> GetWidgetCategory1(int id);

        /// <summary>
        /// Asynchronously saves a WidgetCategory1 entity in the repository. If the WidgetCategory1 already exists, it is updated; otherwise, it is created.
        /// </summary>
        /// <param name="widgetCategory1">The WidgetCategory1 entity to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated WidgetCategory1 entity.</returns>
        Task<WidgetCategory1> SaveWidgetCategory1(WidgetCategory1 widgetCategory1);
    }

    /// <summary>
    /// Repository implementation for managing WidgetCategory1s, inheriting from the base repository.
    /// </summary>
    public class WidgetCategoryRepository : RepositoryBase<WidgetCategory1>, IWidgetCategoryRepository
    {
        /// <summary>
        /// Asynchronously saves a WidgetCategory1 entity in the repository. If the WidgetCategory1 already exists (based on the WidgetCategory1Id), it is updated; otherwise, a new WidgetCategory1 is created.
        /// </summary>
        /// <param name="WidgetCategory1">The WidgetCategory1 entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated WidgetCategory1 entity.</returns>
        public async Task<WidgetCategory1> SaveWidgetCategory1(WidgetCategory1 widgetCategory1)
        {
            var exists = widgetCategory1.Id != null && widgetCategory1.Id > 0;
            if (exists)
                await UpdateAsync(widgetCategory1);
            else
                await CreateAsync(widgetCategory1);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.Id == widgetCategory1.Id);
        }

        /// <summary>
        /// Asynchronously deletes a WidgetCategory1 entity from the repository.
        /// </summary>
        /// <param name="widgetCategory1">The WidgetCategory1 entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteWidgetCategory1Async(WidgetCategory1 widgetCategory1)
        {
            return await DeleteAsync(widgetCategory1);
        }

        /// <summary>
        /// Asynchronously retrieves a specific WidgetCategory1 by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the WidgetCategory1 to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the WidgetCategory1 entity, or null if the WidgetCategory1 is not found.</returns>
        public async Task<WidgetCategory1> GetWidgetCategory1(int id)
        {
            var widgetCategory1s = await ReadAsync();
            return widgetCategory1s.SingleOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Asynchronously retrieves all WidgetCategory1s from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of WidgetCategory1s.</returns>
        public async Task<IEnumerable<WidgetCategory1>> GetAllWidgetCategory1sAsync()
        {
            return await ReadAsync();
        }
    }
}
