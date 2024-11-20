using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Interface that defines operations for managing WidgetCategories.
    /// </summary>
    public interface IWidgetCategoriesRepository
    {
        /// <summary>
        /// Asynchronously deletes a WidgetCategory from the repository.
        /// </summary>
        /// <param name="WidgetCategory">The WidgetCategory entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteWidgetCategoryAsync(WidgetCategory widgetCategory);

        /// <summary>
        /// Asynchronously retrieves all WidgetCategories from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of WidgetCategories.</returns>
        Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync();

        /// <summary>
        /// Asynchronously retrieves a specific WidgetCategory by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the WidgetCategory to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the WidgetCategory entity, or null if the WidgetCategory is not found.</returns>
        Task<WidgetCategory> GetWidgetCategoryAsync(int id);

        /// <summary>
        /// Asynchronously saves a WidgetCategory entity in the repository. If the WidgetCategory already exists, it is updated; otherwise, it is created.
        /// </summary>
        /// <param name="WidgetCategory">The WidgetCategory entity to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated WidgetCategory entity.</returns>
        Task<WidgetCategory> SaveWidgetCategoryAsync(WidgetCategory widgetCategory);
    }

    /// <summary>
    /// Repository implementation for managing WidgetCategories, inheriting from the base repository.
    /// </summary>
    public class WidgetCategoriesRepository : RepositoryBase<WidgetCategory>, IWidgetCategoriesRepository
    {
        /// <summary>
        /// Asynchronously saves a WidgetCategory entity in the repository. If the WidgetCategory already exists (based on the WidgetCategoryId), it is updated; otherwise, a new WidgetCategory is created.
        /// </summary>
        /// <param name="WidgetCategory">The WidgetCategory entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated WidgetCategory entity.</returns>
        public async Task<WidgetCategory> SaveWidgetCategoryAsync(WidgetCategory widgetCategory)
        {
            var exists = widgetCategory.CategoryId != null && widgetCategory.CategoryId > 0;
            if (exists)
                await UpdateAsync(widgetCategory);
            else
                await CreateAsync(widgetCategory);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.CategoryId == widgetCategory.CategoryId);
        }

        /// <summary>
        /// Asynchronously deletes a WidgetCategory entity from the repository.
        /// </summary>
        /// <param name="WidgetCategory">The WidgetCategory entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteWidgetCategoryAsync(WidgetCategory widgetCategory)
        {
            return await DeleteAsync(widgetCategory);
        }

        /// <summary>
        /// Asynchronously retrieves a specific WidgetCategory by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the WidgetCategory to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the WidgetCategory entity, or null if the WidgetCategory is not found.</returns>
        public async Task<WidgetCategory> GetWidgetCategoryAsync(int id)
        {
            var widgetCategories = await ReadAsync();
            return widgetCategories.SingleOrDefault(u => u.CategoryId == id);
        }

        /// <summary>
        /// Asynchronously retrieves all WidgetCategories from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of WidgetCategories.</returns>
        public async Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync()
        {
            return await ReadAsync();
        }
    }
}
