using PAWPMD.Models.DTOS;
using PAWPMD.Models.Enums;
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
    /// Defines the context for saving widgets using different strategies based on the widget type.
    /// </summary>
    public interface IWidgetStContext
    {
        /// <summary>
        /// Saves a widget using the appropriate strategy based on the widget type.
        /// </summary>
        /// <param name="widgetDTO">The DTO representing the widget to be saved.</param>
        /// <param name="userId">The user ID associated with the widget, or null if no user is associated.</param>
        /// <param name="widgetId">The widget ID, or null if the widget is being created.</param>
        /// <param name="widgetType">The type of the widget, which determines the strategy to be used.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="WidgetResponseDTO"/> as the result.</returns>
        Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId, WidgetType widgetType);
    }

    /// <summary>
    /// Context class for saving widgets using a strategy pattern. It delegates the saving of different types of widgets 
    /// to their respective strategy classes based on the <see cref="WidgetType"/> provided.
    /// </summary>
    public class WidgetStContext : IWidgetStContext
    {
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IApiNinjaService _apiNinjaService;
        private readonly IApiNewsService _apiNewsService;
        private readonly IApiPexelsService _apiPexelsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetStContext"/> class with the specified service dependencies.
        /// </summary>
        /// <param name="widgetService">The widget service for managing widgets.</param>
        /// <param name="userWidgetService">The user widget service for managing user-specific widgets.</param>
        /// <param name="widgetSettingService">The widget setting service for managing widget settings.</param>
        /// <param name="openWeatherService">The service for interacting with OpenWeather API.</param>
        /// <param name="apiNinjaService">The service for interacting with API Ninja.</param>
        /// <param name="apiNewsService">The service for interacting with news API.</param>
        /// <param name="apiPexelsService">The service for interacting with Pexels API.</param>
        public WidgetStContext(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IOpenWeatherService openWeatherService, IApiNinjaService apiNinjaService, IApiNewsService apiNewsService, IApiPexelsService apiPexelsService)
        {
            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
            _openWeatherService = openWeatherService;
            _apiNinjaService = apiNinjaService;
            _apiNewsService = apiNewsService;
            _apiPexelsService = apiPexelsService;
        }

        /// <summary>
        /// Saves a widget asynchronously based on the widget type. It selects the appropriate strategy for the specified widget type.
        /// </summary>
        /// <param name="widgetDTO">The DTO representing the widget to be saved.</param>
        /// <param name="userId">The user ID associated with the widget, or null if no user is associated.</param>
        /// <param name="widgetId">The widget ID, or null if the widget is being created.</param>
        /// <param name="widgetType">The type of the widget, which determines the strategy to be used.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="WidgetResponseDTO"/> as the result.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid widget type is provided.</exception>
        public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId, WidgetType widgetType)
        {
            IWidgetStrategy strategy;

            switch (widgetType)
            {
                case WidgetType.Image:
                    strategy = new ImageWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _apiPexelsService);
                    break;
                case WidgetType.News:
                    strategy = new NewsWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _apiNewsService);
                    break;

                case WidgetType.CityDetails:
                    strategy = new CityDetailsWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _apiNinjaService);
                    break;
                case WidgetType.Weather:
                    strategy = new WeatherWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _openWeatherService, _apiNinjaService);
                    break;
                default:
                    throw new ArgumentException("Invalid widget type");
            }
            return await strategy.SaveWidgetAsync(widgetDTO, userId, widgetId);
        }
    }
}
