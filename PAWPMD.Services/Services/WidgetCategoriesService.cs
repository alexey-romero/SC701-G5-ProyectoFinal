using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    public interface IWidgetCategoriesService
    {
        Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);
        Task<bool> DeleteWidgetCategoryAsync(int id);
        Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();
        Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync();
        Task<UserWidget> GetUserWidget(int id);
        Task<WidgetCategory> GetWidgetCategoryAsync(int id);
        Task<UserWidget> SaveUserWidget(UserWidget userWidget);
        Task<WidgetCategory> SaveWidgetCategoryAsync(WidgetCategory widgetCategory);
    }

    /// <summary>
    /// Service for managing widget categories. It provides methods to retrieve, save, delete, and list widget categories. 
    /// It delegates operations to the underlying repository, <see cref="IWidgetCategoriesRepository"/>.
    /// </summary>
    public class WidgetCategoriesService : IWidgetCategoriesService
    {
        private readonly IWidgetCategoriesRepository _widgetCategoriesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetCategoriesService"/> class.
        /// </summary>
        /// <param name="widgetCategoriesRepository">The repository to interact with widget category data.</param>
        public WidgetCategoriesService(IWidgetCategoriesRepository widgetCategoriesRepository)
        {
            _widgetCategoriesRepository = widgetCategoriesRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a widget category by its ID.
        /// </summary>
        /// <param name="id">The ID of the widget category to retrieve.</param>
        /// <returns>A <see cref="WidgetCategory"/> object representing the widget category, or <c>null</c> if not found.</returns>
        public async Task<WidgetCategory> GetWidgetCategoryAsync(int id)
        {
            return await _widgetCategoriesRepository.GetWidgetCategoryAsync(id);
        }

        /// <summary>
        /// Asynchronously saves a new or existing widget category.
        /// </summary>
        /// <param name="widgetCategory">The widget category to save.</param>
        /// <returns>The saved <see cref="WidgetCategory"/> object.</returns>
        public async Task<WidgetCategory> SaveWidgetCategoryAsync(WidgetCategory widgetCategory)
        {
            return await _widgetCategoriesRepository.SaveWidgetCategoryAsync(widgetCategory);
        }

        /// <summary>
        /// Asynchronously deletes a widget category by its ID.
        /// </summary>
        /// <param name="id">The ID of the widget category to delete.</param>
        /// <returns><c>true</c> if the widget category was successfully deleted; otherwise, <c>false</c>.</returns>
        public async Task<bool> DeleteWidgetCategoryAsync(int id)
        {
            var widgetCategory = await _widgetCategoriesRepository.GetAllWidgetCategoriesAsync();
            var deletion = widgetCategory.SingleOrDefault(x => x.CategoryId == id);
            return await _widgetCategoriesRepository.DeleteWidgetCategoryAsync(deletion);
        }

        /// <summary>
        /// Asynchronously retrieves all widget categories.
        /// </summary>
        /// <returns>A collection of <see cref="WidgetCategory"/> objects.</returns>
        public async Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync()
        {
            return await _widgetCategoriesRepository.GetAllWidgetCategoriesAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a user widget by its ID. This method is not yet implemented.
        /// </summary>
        /// <param name="id">The ID of the user widget to retrieve.</param>
        /// <returns>A <see cref="UserWidget"/> object representing the user widget.</returns>
        /// <exception cref="NotImplementedException">Thrown when this method is not yet implemented.</exception>
        public Task<UserWidget> GetUserWidget(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously saves a user widget. This method is not yet implemented.
        /// </summary>
        /// <param name="userWidget">The user widget to save.</param>
        /// <returns>A <see cref="UserWidget"/> object representing the saved user widget.</returns>
        /// <exception cref="NotImplementedException">Thrown when this method is not yet implemented.</exception>
        public Task<UserWidget> SaveUserWidget(UserWidget userWidget)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously deletes a user widget. This method is not yet implemented.
        /// </summary>
        /// <param name="userWidget">The user widget to delete.</param>
        /// <returns><c>true</c> if the user widget was successfully deleted; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException">Thrown when this method is not yet implemented.</exception>
        public Task<bool> DeleteUserWidgetAsync(UserWidget userWidget)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously retrieves all user widgets. This method is not yet implemented.
        /// </summary>
        /// <returns>A collection of <see cref="UserWidget"/> objects representing all user widgets.</returns>
        /// <exception cref="NotImplementedException">Thrown when this method is not yet implemented.</exception>
        public Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync()
        {
            throw new NotImplementedException();
        }
    }

}
