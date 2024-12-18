using Newtonsoft.Json.Linq;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public class CityDetailsWidgetViewStrategy : IWidgetViewStrategy
    {
        public CityDetailsWidgetModel GetCityDetailsModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            var json = setting.Settings.ToString();
            var jObject = JObject.Parse(json);

            var cityData = jObject["cityData"]; 

            if (cityData == null)
            {
                return new CityDetailsWidgetModel();
            }

            return new CityDetailsWidgetModel
            {
                Location = cityData["name"]?.ToString(),  // Ciudad
                Latitude = cityData["latitude"]?.ToObject<double>() ?? 0,  // Latitud
                Longitude = cityData["longitude"]?.ToObject<double>() ?? 0,  // Longitud
                Country = cityData["country"]?.ToString(),  // País
                Population = cityData["population"]?.ToObject<int>() ?? 0,  // Población
                IsCapital = cityData["is_capital"]?.ToObject<bool>() ?? false,  // Es capital
                WidgetId = widgetResponseDTO.Widget.WidgetId  // ID del widget
            };
        }

        public ImageWidgetModel GetImageModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public NewsWidgetModel GetNewsWidgetModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public WeatherWidgetModel GetWeatherModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public void RenderWidget(WidgetResponseDTO widgetResponseDTO, WidgetSetting widgetSetting)
        {
            throw new NotImplementedException();
        }
    }
}
