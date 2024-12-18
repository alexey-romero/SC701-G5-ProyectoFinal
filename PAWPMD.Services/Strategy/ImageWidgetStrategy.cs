using Newtonsoft.Json.Linq;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers.DTOS;
using PAWPMD.Service.Services;
using PAWPMD.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Strategy
{ /// <summary>
  /// Strategy class for handling image widget operations.
  /// Implements the IWidgetStrategy interface for saving widgets, specifically for image-based widgets.
  /// </summary>
    public class ImageWidgetStrategy : IWidgetStrategy
    {
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        private readonly IApiPexelsService _apiPexelsService;

        /// <summary>
        /// Constructor that initializes the ImageWidgetStrategy class with the necessary services.
        /// </summary>
        /// <param name="widgetService">Service responsible for handling widgets.</param>
        /// <param name="userWidgetService">Service responsible for handling user widgets.</param>
        /// <param name="widgetSettingService">Service responsible for handling widget settings.</param>
        /// <param name="pexelsService">Service for interacting with the Pexels API to fetch images.</param>
        public ImageWidgetStrategy(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IApiPexelsService pexelsService)
        {
            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
            _apiPexelsService = pexelsService;
        }

        /// <summary>
        /// Saves an image widget asynchronously, including fetching image data from the Pexels API.
        /// </summary>
        /// <param name="widgetDTO">The data transfer object containing widget details.</param>
        /// <param name="userId">The optional ID of the user associated with the widget.</param>
        /// <param name="widgetId">The optional ID of an existing widget to update.</param>
        /// <returns>A <see cref="WidgetResponseDTO"/> containing the saved widget's response data.</returns>
        public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId)
        {
            try
            {
                // Save the widget and user widget, and fetch widget settings.
                var widget = await _widgetService.SaveWidgetAsync(widgetDTO, userId, null);
                var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetDTO, widget, userId);
                var widgetSettingsJson = JObject.Parse(widgetDTO.WidgetSetting.Settings);

                // Fetch and append image data to the widget settings.
                var imagesData = await FetchAndAppendImageDataAsync(widgetSettingsJson);

                // Update the widget settings and save them.
                widgetDTO.WidgetSetting.Settings = widgetSettingsJson.ToString();
                widgetDTO.WidgetSetting.UserWidgetId = userWidget.UserWidgetId;

                // Save widget settings.
                var saveSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetDTO, userWidget);

                // Prepare the widget response and return it.
                var widgetResponseDTO = await PrepareWidgetResponseAsync(widget, userWidget, widgetDTO.WidgetSetting);

                return widgetResponseDTO;
            }
            catch (PAWPMDException)
            {
                // Rethrow custom exceptions.
                throw;
            }
        }

        /// <summary>
        /// Fetches image data based on the query from the Pexels API and appends it to the widget settings.
        /// </summary>
        /// <param name="widgetSettingsJson">The JSON object representing the widget settings.</param>
        /// <returns>A JSON string containing the image data fetched from the Pexels API.</returns>
        public async Task<string> FetchAndAppendImageDataAsync(JObject widgetSettingsJson)
        {
            // Get the query string from the widget settings.
            var querie = widgetSettingsJson["querie"].ToString();

            // Fetch image data using the Pexels API.
            var imagesDataJson = await _apiPexelsService.GetImagesByQuerie(querie, "tcwczHVxnNrwhnuQSNjttPYmv4xQqQ6ATvbGPlDHzXcnry38QEVzPv0t");

            // Parse and append the image data to the widget settings.
            var imagesData = JObject.Parse(imagesDataJson);
            widgetSettingsJson["images"] = imagesData;

            return imagesDataJson;
        }

        /// <summary>
        /// Prepares and returns the widget response DTO based on the provided widget, user widget, and widget settings.
        /// </summary>
        /// <param name="widget">The widget object.</param>
        /// <param name="userWidget">The user widget object.</param>
        /// <param name="widgetSettingsDTO">The widget settings DTO.</param>
        /// <returns>A <see cref="WidgetResponseDTO"/> containing the widget response data.</returns>
        private async Task<WidgetResponseDTO> PrepareWidgetResponseAsync(Widget widget, UserWidget userWidget, WidgetSettingDTO widgetSettingsDTO)
        {
            var widgetSettings = new WidgetSetting
            {
                Settings = widgetSettingsDTO.Settings
            };

            // Prepare and return the widget response.
            return await WidgetDTOResponseMapper.PrepareWidgetDTOReponseDataAsync(
                new WidgetResponseDTO
                {
                    Widget = new WidgetDTO(),
                    UserWidget = new UserWidgetDTO(),
                    WidgetSetting = widgetSettingsDTO
                },
                widget,
                userWidget,
                widgetSettings
            );
        }
    }
}
