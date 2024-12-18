using Newtonsoft.Json.Linq;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public class NewsWidgetViewStrategy : IWidgetViewStrategy
    {

        public NewsWidgetModel GetNewsWidgetModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            var json = setting.Settings.ToString() ;


            var jObject = JObject.Parse(json);

            var newsData = jObject["newsData"];

            var articles = newsData["articles"]?.ToList(); 

            if(articles == null && articles.Count > 0)
            {
                return new NewsWidgetModel();
            }

            var randomArticle = articles[new Random().Next(articles.Count)];

            return new NewsWidgetModel
            {
                SourceName = randomArticle["source"]?["name"]?.ToString(),
                Author = randomArticle["author"]?.ToString(),
                Title = randomArticle["title"]?.ToString(),
                Description = randomArticle["description"]?.ToString(),
                Url = randomArticle["url"]?.ToString(),
                UrlToImage = randomArticle["urlToImage"]?.ToString(),
                PublishedAt = randomArticle["publishedAt"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                Content = randomArticle["content"]?.ToString(),
                WidgetId = widgetResponseDTO.Widget.WidgetId
            };
        }

        public CityDetailsWidgetModel GetCityDetailsModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public ImageWidgetModel GetImageModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
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
