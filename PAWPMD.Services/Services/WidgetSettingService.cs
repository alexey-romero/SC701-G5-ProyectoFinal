using PAWPMD.Data.Repository;
using PAWPMD.Models.DTOS;
using PAWPMD.Models;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Service.Mappers;
using Microsoft.Identity.Client;


namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface defining the contract for widget setting services.
    /// </summary>
    public interface IWidgetSettingService
    {
        /// <summary>
        /// Asynchronously deletes a widget setting.
        /// </summary>
        /// <param name="widgetSettingDTO">The widget setting data transfer object.</param>
        /// <param name="userWidgetId">The ID of the user widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteWidgetSettingAsync(WidgetRequestDTO widgetRequestDTO, UserWidget userWidget);

        /// <summary>
        /// Asynchronously retrieves all widget settings from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget settings.</returns>
        Task<IEnumerable<WidgetSetting>> GetAllWidgetSettingsAsync();

        /// <summary>
        /// Asynchronously retrieves a widget setting by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget setting to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget setting entity if found, otherwise null.</returns>
        Task<WidgetSetting> GetWidgetSettingByIdAsync(int id);

        /// <summary>
        /// Asynchronously saves a widget setting entity. If the widget setting already exists, it updates it; otherwise, it creates a new widget setting.
        /// </summary>
        /// <param name="widgetSettingDTO">The widget setting data transfer object.</param>
        /// <param name="userWidgetId">The ID of the user widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated widget setting entity.</returns>
        Task<WidgetSetting> SaveWidgetSettinsAsync(WidgetRequestDTO widgetRequestDTO, UserWidget userWidget);
        
        /// <summary>
        /// asynchronusly retrieves a widget setting by user widget id
        /// </summary>
        /// <param name="userWidgetId">The ID of the user widget.</param>
        Task<WidgetSetting> GetWidgetSettingByUserWidgetIdAsync(int userWidgetId);
    }

    /// <summary>
    /// Service class for managing widget settings.
    /// </summary>
    public class WidgetSettingService : IWidgetSettingService
    {
        private readonly IWidgetSettingRepository _widgetSettingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetSettingService"/> class.
        /// </summary>
        /// <param name="widgetSettingRepository">The widget setting repository.</param>
        public WidgetSettingService(IWidgetSettingRepository widgetSettingRepository)
        {
            _widgetSettingRepository = widgetSettingRepository;
        }

        /// <summary>
        /// Asynchronously saves a widget setting entity. If the widget setting already exists, it updates it; otherwise, it creates a new widget setting.
        /// </summary>
        /// <param name="widgetSettingDTO">The widget setting data transfer object.</param>
        /// <param name="userWidgetId">The ID of the user widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated widget setting entity.</returns>
        public async Task<WidgetSetting> SaveWidgetSettinsAsync(WidgetRequestDTO widgetRequestDTO, UserWidget userWidget)

        {

            var widgetSetting = await _widgetSettingRepository.GetWidgetSettingAsync(widgetRequestDTO.WidgetSetting.WidgetSettingsId);

            widgetSetting = await WidgetSettingMapper.PrepareWidgetSettingData(widgetSetting, widgetRequestDTO, userWidget);

            var savedWidgetSetting = await _widgetSettingRepository.SaveWidgetSettingAsync(widgetSetting);

            if ( savedWidgetSetting== null || savedWidgetSetting.WidgetSettingsId <= 0)
            {
                throw new PAWPMDException("Failed to save the WidgetSetting.");
            }
            return savedWidgetSetting;
        }

        /// <summary>
        /// Asynchronously retrieves a widget setting by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget setting to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget setting entity if found, otherwise null.</returns>
        public async Task<WidgetSetting> GetWidgetSettingByIdAsync(int id)
        {
            return await _widgetSettingRepository.GetWidgetSettingAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all widget settings from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget settings.</returns>
        public async Task<IEnumerable<WidgetSetting>> GetAllWidgetSettingsAsync()
        {
            return await _widgetSettingRepository.GetAllWidgetSettingsAsync();
        }

        /// <summary>
        /// Asynchronously deletes a widget setting from the system.
        /// </summary>
        /// <param name="widgetSettingDTO">The widget setting data transfer object.</param>
        /// <param name="userWidgetId">The ID of the user widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteWidgetSettingAsync(WidgetRequestDTO widgetRequestDTO, UserWidget userWidget)
        {
            var widgetSetting = await _widgetSettingRepository.GetWidgetSettingAsync(widgetRequestDTO.WidgetSetting.WidgetSettingsId);

            if (widgetSetting == null || widgetSetting.WidgetSettingsId <= 0)
            {
                throw new PAWPMDException("Widget setting not found.");
            }

            return await _widgetSettingRepository.DeleteWidgetSettingAsync(widgetSetting);
        }

        /// <summary>
        /// asynchronusly retrieves a widget setting by user widget id
        /// </summary>
        /// <param name="userWidgetId">The ID of the user widget.</param>
        public async Task<WidgetSetting> GetWidgetSettingByUserWidgetIdAsync(int userWidgetId)
        {
            return await _widgetSettingRepository.GetWidgetSettingByUserWidgetIdAsync(userWidgetId);
        }

    }
}
