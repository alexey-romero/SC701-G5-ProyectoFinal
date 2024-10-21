using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Interface that defines operations for managing Widgets.
    /// </summary>
    public interface IWidgetRepository
    {
        /// <summary>
        /// Asynchronously deletes a Widget from the repository.
        /// </summary>
        /// <param name="Widget">The Widget entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteWidgetAsync(Widget widget);

        /// <summary>
        /// Asynchronously retrieves all Widgets from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of Widgets.</returns>
        Task<IEnumerable<Widget>> GetAllWidgetsAsync();

        /// <summary>
        /// Asynchronously retrieves a specific Widget by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the Widget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Widget entity, or null if the Widget is not found.</returns>
        Task<Widget> GetWidget(int id);

        /// <summary>
        /// Asynchronously saves a Widget entity in the repository. If the Widget already exists, it is updated; otherwise, it is created.
        /// </summary>
        /// <param name="Widget">The Widget entity to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated Widget entity.</returns>
        Task<Widget> SaveWidget(Widget widget);
    }

    /// <summary>
    /// Repository implementation for managing Widgets, inheriting from the base repository.
    /// </summary>
    public class WidgetRepository : RepositoryBase<Widget>, IWidgetRepository
    {
        /// <summary>
        /// Asynchronously saves a Widget entity in the repository. If the Widget already exists (based on the WidgetId), it is updated; otherwise, a new Widget is created.
        /// </summary>
        /// <param name="Widget">The Widget entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated Widget entity.</returns>
        public async Task<Widget> SaveWidget(Widget widget)
        {
            var exists = widget.WidgetId != null && widget.WidgetId > 0;
            if (exists)
                await UpdateAsync(widget);
            else
                await CreateAsync(widget);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.WidgetId == widget.WidgetId);
        }

        /// <summary>
        /// Asynchronously deletes a Widget entity from the repository.
        /// </summary>
        /// <param name="Widget">The Widget entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteWidgetAsync(Widget widget)
        {
            return await DeleteAsync(widget);
        }

        /// <summary>
        /// Asynchronously retrieves a specific Widget by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the Widget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Widget entity, or null if the Widget is not found.</returns>
        public async Task<Widget> GetWidget(int id)
        {
            var widgets = await ReadAsync();
            return widgets.SingleOrDefault(u => u.WidgetId == id);
        }

        /// <summary>
        /// Asynchronously retrieves all Widgets from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of Widgets.</returns>
        public async Task<IEnumerable<Widget>> GetAllWidgetsAsync()
        {
            return await ReadAsync();
        }
    }
}
