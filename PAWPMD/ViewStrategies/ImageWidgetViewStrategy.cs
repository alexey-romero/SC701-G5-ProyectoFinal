using Newtonsoft.Json.Linq;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public class ImageWidgetViewStrategy : IWidgetViewStrategy
    {
        public CityDetailsWidgetModel GetCityDetailsModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            throw new NotImplementedException();
        }

        public ImageWidgetModel GetImageModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            var json = setting.Settings.ToString();
            var jObject = JObject.Parse(json);
            var imageData = jObject["image"];

            return new ImageWidgetModel
            {
                Id = (int)widgetResponseDTO.Widget.WidgetId,
                PhotographerName = imageData["photographerName"]?.ToString(),
                PhotographerUrl = imageData["photographerUrl"]?.ToString(),
                ImageAltText = imageData["imageAltText"]?.ToString(),
                AvgColor = imageData["avgColor"]?.ToString(),
                OriginalUrl = imageData["originalUrl"]?.ToString(),
                LargeUrl = imageData["largeUrl"]?.ToString(),
                MediumUrl = imageData["mediumUrl"]?.ToString(),
                SmallUrl = imageData["smallUrl"]?.ToString(),
                PortraitUrl = imageData["portraitUrl"]?.ToString(),
                LandscapeUrl = imageData["landscapeUrl"]?.ToString(),
                TinyUrl = imageData["tinyUrl"]?.ToString(),
                WidgetId = widgetResponseDTO.Widget.WidgetId
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
            // Implementación personalizada para renderizar el widget de imagen.
            throw new NotImplementedException();
        }
    }
}
