using Newtonsoft.Json.Linq;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public class NewsWidgetViewStrategy : IWidgetViewStrategy
    {
        public CityDetailsWidgetModel GetCityDetailsModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public ImageWidgetModel GetImageModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public NewsWidgetModel GetNewsModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            var json = setting.Settings.ToString();
            var jObject = JObject.Parse(json);
            var newsData = jObject["news"];

            return new NewsWidgetModel
            {
                SourceName = newsData["sourceName"]?.ToString(),
                Author = newsData["author"]?.ToString(),
                Title = newsData["title"]?.ToString(),
                Description = newsData["description"]?.ToString(),
                Url = newsData["url"]?.ToString(),
                UrlToImage = newsData["urlToImage"]?.ToString(),
                PublishedAt = newsData["publishedAt"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                Content = newsData["content"]?.ToString()
            };
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
            // Implementación personalizada para renderizar el widget de noticias.
            throw new NotImplementedException();
        }
    }
}
