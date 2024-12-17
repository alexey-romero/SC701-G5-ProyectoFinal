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

public class WeatherWidgetStrategy : IWidgetStrategy
{
    private readonly IWidgetService _widgetService;
    private readonly IUserWidgetService _userWidgetService;
    private readonly IWidgetSettingService _widgetSettingService;
    private readonly IOpenWeatherService _openWeatherService;
    private readonly IApiNinjaService _apiNinjaService;
    public WeatherWidgetStrategy(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IOpenWeatherService openWeatherService, IApiNinjaService apiNinjaService)
    {
        _widgetService = widgetService;
        _userWidgetService = userWidgetService;
        _widgetSettingService = widgetSettingService;
        _openWeatherService = openWeatherService;
        _apiNinjaService = apiNinjaService;
    }

    public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId)
    {
        try
        {
            var widget = await _widgetService.SaveWidgetAsync(widgetDTO, userId, null);
            var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetDTO, widget, userId);


            var widgetSettingsJson = JObject.Parse(widgetDTO.WidgetSetting.Settings);


            var weatherData = await FetchAndAppendWeatherDataAsync(widgetSettingsJson);

          
            widgetDTO.WidgetSetting.Settings = widgetSettingsJson.ToString();
            widgetDTO.WidgetSetting.UserWidgetId = userWidget.UserWidgetId;
         
            var saveSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetDTO, userWidget);


            var widgetResponseDTO = await PrepareWidgetResponseAsync(widget, userWidget, widgetDTO.WidgetSetting);

            return widgetResponseDTO;
        }
        catch (PAWPMDException)
        {
            throw; 
        }
    }

    private async Task<string> FetchAndAppendWeatherDataAsync(JObject widgetSettingsJson)
    {
      
        //now we catch the city name of the widgetSettingsJson
        var city = widgetSettingsJson["city"]?.ToString();
        //now we invoque the apiNinjaService to get the city details
        var cityDetailsJson = await _apiNinjaService.GetCityDetailsAsync(city, "/bzxXkCH+bHFrHefMQgdMg==s3O310qglO4sx1to");

        var cityDetails = JObject.Parse(cityDetailsJson);

        var latString = cityDetails["latitude"]?.ToString();
        var lonString = cityDetails["longitude"]?.ToString();

        if (!double.TryParse(latString, out double lat))
        {
            throw new PAWPMDException("Invalid latitude value");
        }

        if (!double.TryParse(lonString, out double lon))
        {
            throw new PAWPMDException("Invalid longitude value");
        }
        var apiKey = "n4fKTxjoe9sNzmF3";


        var weatherDataJson = await _openWeatherService.GetWeatherDataAsync(lat, lon, apiKey);
        var weatherData = JObject.Parse(weatherDataJson);

        widgetSettingsJson["weather"] = weatherData;

        return weatherDataJson;
    }

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
