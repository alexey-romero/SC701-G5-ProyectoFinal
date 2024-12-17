using Newtonsoft.Json.Linq;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public class WeatherWidgetViewStrategy : IWidgetViewStrategy
    {
        public WeatherWidgetModel GetWeatherModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            var json = setting.Settings.ToString();
            var jObject = JObject.Parse(json);
            var weatherData = jObject["weather"];
            var dataDay = weatherData["data_day"];
            var name = weatherData["metadata"]?["name"]?.ToString();
            var currentTemperature = dataDay["temperature_instant"]?[0]?.ToObject<double>() ?? 0;
            var maxTemperature = dataDay["temperature_max"]?.Max()?.ToObject<double>() ?? 0;
            var minTemperature = dataDay["temperature_min"]?.Min()?.ToObject<double>() ?? 0;
            var precipitationProbability = dataDay["precipitation_probability"]?[0]?.ToObject<int>() ?? 0;
            var humidity = dataDay["relativehumidity_mean"]?[0]?.ToObject<int>() ?? 0;

            return new WeatherWidgetModel
            {
                Location = name,
                CurrentTemperature = currentTemperature,
                MaxTemperature = maxTemperature,
                MinTemperature = minTemperature,
                Humidity = humidity,
                PrecipitationProbability = precipitationProbability,
                WidgetId = widgetResponseDTO.Widget.WidgetId 
            };
        }

        public void RenderWidget(WidgetResponseDTO widgetResponseDTO, WidgetSetting widgetSetting)
        {
            throw new NotImplementedException();
        }
    }
}
