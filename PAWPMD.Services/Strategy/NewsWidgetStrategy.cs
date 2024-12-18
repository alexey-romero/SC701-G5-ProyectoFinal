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
{
    /// <summary>
    /// Represents the strategy for handling news widget operations.
    /// Implements the <see cref="IWidgetStrategy"/> interface for saving and preparing a news widget.
    /// </summary>
    public class NewsWidgetStrategy : IWidgetStrategy
    {
        // Services used within the strategy
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        private readonly IApiNewsService _apiNewsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsWidgetStrategy"/> class with the required services.
        /// </summary>
        /// <param name="widgetService">Service to manage widgets.</param>
        /// <param name="userWidgetService">Service to manage user widgets.</param>
        /// <param name="widgetSettingService">Service to manage widget settings.</param>
        /// <param name="apiNewsService">Service to fetch news data via an API.</param>
        public NewsWidgetStrategy(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IApiNewsService apiNewsService)
        {
            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
            _apiNewsService = apiNewsService;
        }

        /// <summary>
        /// Saves a widget asynchronously. Fetches and appends news data to the widget settings, and returns a response DTO.
        /// </summary>
        /// <param name="widgetDTO">The data transfer object containing widget information.</param>
        /// <param name="userId">The optional ID of the user to whom the widget belongs.</param>
        /// <param name="widgetId">The optional ID of the widget being edited.</param>
        /// <returns>A <see cref="WidgetResponseDTO"/> containing the widget's response data.</returns>
        /// <exception cref="PAWPMDException">Thrown if any error occurs during widget save process.</exception>
        public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId)
        {
            try
            {
                // Save the widget and associated user widget
                var widget = await _widgetService.SaveWidgetAsync(widgetDTO, userId, null);
                var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetDTO, widget, userId);

                // Parse the widget settings from JSON
                var widgetSettingsJson = JObject.Parse(widgetDTO.WidgetSetting.Settings);

                // Fetch and append news data based on query in widget settings
                var newsData = await FetchAndAppendNewsDataAsync(widgetSettingsJson);

                // Update widget settings with the new news data
                widgetDTO.WidgetSetting.Settings = widgetSettingsJson.ToString();
                widgetDTO.WidgetSetting.UserWidgetId = userWidget.UserWidgetId;

                // Save widget settings
                var saveSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetDTO, userWidget);

                // Prepare and return the widget response DTO
                var widgetResponseDTO = await PrepareWidgetResponseAsync(widget, userWidget, widgetDTO.WidgetSetting);

                return widgetResponseDTO;
            }
            catch (PAWPMDException)
            {
                throw; // Rethrow the exception if one occurs
            }
        }

        /// <summary>
        /// Fetches news data based on the query from the widget settings and appends it to the settings.
        /// </summary>
        /// <param name="widgetSettingsJson">The JSON object containing widget settings.</param>
        /// <returns>A JSON string containing the fetched news data.</returns>
        private async Task<string> FetchAndAppendNewsDataAsync(JObject widgetSettingsJson)
        {
            // Get the query parameter from widget settings
            var querie = widgetSettingsJson["querie"].ToString();

            // Fetch news data based on the query from the news API
            var newsDataJson = await _apiNewsService.GetNewsByQuerie(querie, "aad559e0f77042af87ad27c82e767315");

            // Parse and append the news data to widget settings
            var newsData = JObject.Parse(newsDataJson);
            widgetSettingsJson["newsData"] = newsData;

            return newsDataJson;
        }

        /// <summary>
        /// Prepares the response DTO for the widget, including the widget, user widget, and widget settings.
        /// </summary>
        /// <param name="widget">The widget object to be included in the response.</param>
        /// <param name="userWidget">The user widget object to be included in the response.</param>
        /// <param name="widgetSettingsDTO">The widget settings DTO to be included in the response.</param>
        /// <returns>A <see cref="WidgetResponseDTO"/> containing the widget response data.</returns>
        private async Task<WidgetResponseDTO> PrepareWidgetResponseAsync(Widget widget, UserWidget userWidget, WidgetSettingDTO widgetSettingsDTO)
        {
            var widgetSettings = new WidgetSetting
            {
                Settings = widgetSettingsDTO.Settings
            };

            // Map the response data to the WidgetResponseDTO
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
