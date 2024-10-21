using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    /// <summary>
    /// Interface that defines operations for managing Widgets.
    /// </summary>
    public interface IUserWidgetRepository
    {
        /// <summary>
        /// Asynchronously deletes a UserWidget from the repository.
        /// </summary>
        /// <param name="UserWidget">The UserWidget entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);

        /// <summary>
        /// Asynchronously retrieves all UserWidgets from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of UserWidgets.</returns>
        Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();

        /// <summary>
        /// Asynchronously retrieves a specific UserWidget by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the UserWidget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UserWidget entity, or null if the UserWidget is not found.</returns>
        Task<UserWidget> GetUserWidget(int id);

        /// <summary>
        /// Asynchronously saves a UserWidget entity in the repository. If the UserWidget already exists, it is updated; otherwise, it is created.
        /// </summary>
        /// <param name="User Widget">The UserWidget entity to save.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated UserWidget entity.</returns>
        Task<UserWidget> SaveUserWidget(UserWidget userWidget);
    }

    /// <summary>
    /// Repository implementation for managing UserWidgets, inheriting from the base repository.
    /// </summary>
    public class UserWidgetRepository : RepositoryBase<UserWidget>, IUserWidgetRepository
    {
        /// <summary>
        /// Asynchronously saves a UserWidget entity in the repository. If the UserWidget already exists (based on the Id), it is updated; otherwise, a new UserWidget is created.
        /// </summary>
        /// <param name="User Widget">The UserWidget entity to save or update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated UserWidget entity.</returns>
        public async Task<UserWidget> SaveUserWidget(UserWidget userWidget)
        {
            var exists = userWidget.UserWidgetId != null && userWidget.UserWidgetId > 0;
            if (exists)
                await UpdateAsync(userWidget);
            else
                await CreateAsync(userWidget);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.UserWidgetId == userWidget.UserWidgetId);
        }

        /// <summary>
        /// Asynchronously deletes a UserWidget entity from the repository.
        /// </summary>
        /// <param name="UserWidget">The UserWidget entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteUserWidgetAsync(UserWidget userWidget)
        {
            return await DeleteAsync(userWidget);
        }

        /// <summary>
        /// Asynchronously retrieves a specific UserWidget by ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the UserWidget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the UserWidget entity, or null if the UserWidget is not found.</returns>
        public async Task<UserWidget> GetUserWidget(int id)
        {
            var userWidgets = await ReadAsync();
            return userWidgets.SingleOrDefault(u => u.UserWidgetId == id);
        }

        /// <summary>
        /// Asynchronously retrieves all UserWidgets from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of UserWidgets.</returns>
        public async Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync()
        {
            return await ReadAsync();
        }
    }
}
