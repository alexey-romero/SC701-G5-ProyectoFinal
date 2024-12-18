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
            var cityData = jObject["cityDetails"];

            return new CityDetailsWidgetModel
            {
                Location = cityData["location"]?.ToString(),
                Latitude = cityData["latitude"]?.ToObject<double>() ?? 0,
                Longitude = cityData["longitude"]?.ToObject<double>() ?? 0,
                Country = cityData["country"]?.ToString(),
                Population = cityData["population"]?.ToObject<int>() ?? 0,
                IsCapital = cityData["isCapital"]?.ToObject<bool>() ?? false,
                WidgetId = widgetResponseDTO.Widget.WidgetId
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
