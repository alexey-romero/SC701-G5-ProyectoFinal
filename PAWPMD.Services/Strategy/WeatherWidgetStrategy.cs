using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers.DTOS;
using PAWPMD.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PAWPMD.Strategy;

/// <summary>
/// The WeatherWidgetStrategy class implements the IWidgetStrategy interface for handling weather widget logic.
/// It is responsible for saving widgets, fetching weather data, and preparing widget response data.
/// </summary>
public class WeatherWidgetStrategy : IWidgetStrategy
{
    private readonly IWidgetService _widgetService;
    private readonly IUserWidgetService _userWidgetService;
    private readonly IWidgetSettingService _widgetSettingService;
    private readonly IOpenWeatherService _openWeatherService;
    private readonly IApiNinjaService _apiNinjaService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherWidgetStrategy"/> class.
    /// </summary>
    /// <param name="widgetService">The widget service responsible for saving widget data.</param>
    /// <param name="userWidgetService">The user widget service responsible for saving user-specific widget data.</param>
    /// <param name="widgetSettingService">The widget setting service responsible for saving widget settings.</param>
    /// <param name="openWeatherService">The open weather service for fetching weather data.</param>
    /// <param name="apiNinjaService">The API Ninja service for fetching city details.</param>
    public WeatherWidgetStrategy(
        IWidgetService widgetService,
        IUserWidgetService userWidgetService,
        IWidgetSettingService widgetSettingService,
        IOpenWeatherService openWeatherService,
        IApiNinjaService apiNinjaService)
    {
        _widgetService = widgetService;
        _userWidgetService = userWidgetService;
        _widgetSettingService = widgetSettingService;
        _openWeatherService = openWeatherService;
        _apiNinjaService = apiNinjaService;
    }

    /// <summary>
    /// Saves a weather widget, including fetching weather data based on the widget settings.
    /// </summary>
    /// <param name="widgetDTO">The widget request data transfer object containing widget information.</param>
    /// <param name="userId">The user ID associated with the widget.</param>
    /// <param name="widgetId">The widget ID if updating an existing widget; otherwise, null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the widget response DTO.</returns>
    /// <exception cref="PAWPMDException">Thrown when there is an issue processing the widget.</exception>
    public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId)
    {
        try
        {
            // Save the widget and user widget
            var widget = await _widgetService.SaveWidgetAsync(widgetDTO, userId, null);
            var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetDTO, widget, userId);

            // Parse the widget settings JSON to extract necessary information
            var widgetSettingsJson = JObject.Parse(widgetDTO.WidgetSetting.Settings);

            // Fetch and append weather data to the widget settings
            var weatherData = await FetchAndAppendWeatherDataAsync(widgetSettingsJson);

            // Update widget settings with the fetched weather data
            widgetDTO.WidgetSetting.Settings = widgetSettingsJson.ToString();
            widgetDTO.WidgetSetting.UserWidgetId = userWidget.UserWidgetId;

            // Save the updated widget settings
            var saveSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetDTO, userWidget);

            // Prepare the response DTO with the saved data
            var widgetResponseDTO = await PrepareWidgetResponseAsync(widget, userWidget, widgetDTO.WidgetSetting);

            return widgetResponseDTO;
        }
        catch (PAWPMDException)
        {
            throw; // Rethrow any custom exception encountered during the process
        }
    }

    /// <summary>
    /// Fetches and appends weather data based on the widget's settings.
    /// </summary>
    /// <param name="widgetSettingsJson">The widget settings as a JSON object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the fetched weather data in JSON format.</returns>
    /// <exception cref="PAWPMDException">Thrown if the latitude or longitude cannot be parsed correctly.</exception>
    private async Task<string> FetchAndAppendWeatherDataAsync(JObject widgetSettingsJson)
    {
        // Extract the city name from the widget settings
        var city = widgetSettingsJson["city"]?.ToString();

        // Fetch city details using the API Ninja service
        var cityDetailsJson = await _apiNinjaService.GetCityDetailsAsync(city, "/bzxXkCH+bHFrHefMQgdMg==s3O310qglO4sx1to");

        var cityDetails = JObject.Parse(cityDetailsJson);

        // Extract latitude and longitude values
        var latString = cityDetails["latitude"]?.ToString();
        var lonString = cityDetails["longitude"]?.ToString();

        // Parse latitude and longitude values
        if (!double.TryParse(latString, out double lat))
        {
            throw new PAWPMDException("Invalid latitude value");
        }

        if (!double.TryParse(lonString, out double lon))
        {
            throw new PAWPMDException("Invalid longitude value");
        }

        var apiKey = "n4fKTxjoe9sNzmF3";

        // Fetch weather data from the OpenWeather service using the obtained latitude and longitude
        var weatherDataJson = await _openWeatherService.GetWeatherDataAsync(lat, lon, apiKey);
        var weatherData = JObject.Parse(weatherDataJson);

        // Append the weather data to the widget settings
        widgetSettingsJson["weather"] = weatherData;

        return weatherDataJson;
    }

    /// <summary>
    /// Prepares the widget response DTO by mapping widget, user widget, and widget settings data.
    /// </summary>
    /// <param name="widget">The saved widget entity.</param>
    /// <param name="userWidget">The saved user widget entity.</param>
    /// <param name="widgetSettingsDTO">The widget settings data transfer object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the prepared widget response DTO.</returns>
    private async Task<WidgetResponseDTO> PrepareWidgetResponseAsync(Widget widget, UserWidget userWidget, WidgetSettingDTO widgetSettingsDTO)
    {
        var widgetSettings = new WidgetSetting
        {
            Settings = widgetSettingsDTO.Settings
        };

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