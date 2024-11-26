using PAWPMD.Architecture.Exceptions;
using PAWPMD.Data.Repository;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface defining the contract for user widget services.
    /// </summary>
    public interface IUserWidgetService
    {
        /// <summary>
        /// Asynchronously deletes a user widget.
        /// </summary>
        /// <param name="userWidget">The user widget entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);

        /// <summary>
        /// Asynchronously retrieves all user widgets from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of user widgets.</returns>
        Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();

        /// <summary>
        /// Asynchronously retrieves a user widget by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user widget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user widget entity if found, otherwise null.</returns>
        Task<UserWidget> GetUserWidgetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a user widget by its widget ID.
        /// </summary>
        /// <param name="widgetId">The ID of the widget to retrieve.</param>
        Task<UserWidget> GetUserWidgetByWidgetIdAsync(int widgetId);

        /// <summary>
        /// Asynchronously saves a user widget entity. If the user widget already exists, it updates it; otherwise, it creates a new user widget.
        /// </summary>
        /// <param name="userWidgetDTO">The user widget data transfer object.</param>
        /// <param name="userId">The ID of the user creating the widget.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated user widget entity.</returns>
        Task<UserWidget> SaveUserWidgetAsync(WidgetRequestDTO widgetRequestDTO, Widget widget, int? userId);
    
    }

    /// <summary>
    /// Service class for managing user widgets.
    /// </summary>
    public class UserWidgetService : IUserWidgetService
    {
        private readonly IUserWidgetRepository _userWidgetRepository;
        private readonly IWidgetService _widgetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserWidgetService"/> class.
        /// </summary>
        /// <param name="userWidgetRepository">The user widget repository.</param>
        public UserWidgetService(IUserWidgetRepository userWidgetRepository)
        {
            _userWidgetRepository = userWidgetRepository;

        }

        /// <summary>
        /// Asynchronously saves a user widget entity. If the user widget already exists, it updates it; otherwise, it creates a new user widget.
        /// </summary>
        /// <param name="userWidgetDTO">The user widget data transfer object.</param>
        /// <param name="userId">The ID of the user creating the widget.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated user widget entity.</returns>
        public async Task<UserWidget> SaveUserWidgetAsync(WidgetRequestDTO widgetRequestDTO, Widget widget, int? userId)
        {

            var userWidget = await _userWidgetRepository.GetUserWidget(widgetRequestDTO.UserWidget.UserWidgetId);

            userWidget = UserWidgetMapper.PrepareUserWidgetData(userWidget, widget ,widgetRequestDTO, userId);

            var savedUserWidget = await _userWidgetRepository.SaveUserWidget(userWidget);

            if (savedUserWidget == null || savedUserWidget.UserWidgetId <= 0)
            {
                throw new PAWPMDException("Failed to save the UserWidget.");
            }

            return savedUserWidget;
        }

        /// <summary>
        /// Asynchronously deletes a user widget from the system.
        /// </summary>
        /// <param name="userWidget">The user widget entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteUserWidgetAsync(UserWidget userWidget)
        {
            return await _userWidgetRepository.DeleteUserWidgetAsync(userWidget);
        }

        /// <summary>
        /// Asynchronously retrieves all user widgets from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of user widgets.</returns>
        public async Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync()
        {
            return await _userWidgetRepository.GetAllUserWidgetsAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a user widget by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user widget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user widget entity if found, otherwise null.</returns>
        public async Task<UserWidget> GetUserWidgetByIdAsync(int id)
        {
            return await _userWidgetRepository.GetUserWidget(id);
        }

        /// <summary>
        /// Asynchronously retrieves a user widget by its widget ID.
        /// </summary>
        /// <param name="widgetId">The ID of the widget to retrieve.</param
        public async Task<UserWidget> GetUserWidgetByWidgetIdAsync(int widgetId)
        {
            return await _userWidgetRepository.GetUserWidgetByWidgetIdAsync(widgetId);
        }
    }
}
