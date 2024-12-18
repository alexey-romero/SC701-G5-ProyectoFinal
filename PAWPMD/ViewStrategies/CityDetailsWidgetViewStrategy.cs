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

        public void RenderWidget(WidgetResponseDTO widgetResponseDTO, WidgetSetting widgetSetting)
        {
            // Implementación personalizada para renderizar el widget de detalles de la ciudad.
            throw new NotImplementedException();
        }
    }
}
