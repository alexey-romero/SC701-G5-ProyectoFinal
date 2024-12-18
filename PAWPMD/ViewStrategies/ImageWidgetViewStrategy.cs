using Newtonsoft.Json.Linq;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public class ImageWidgetViewStrategy : IWidgetViewStrategy
    {

        public ImageWidgetModel GetImageModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
        {
            var json = setting.Settings.ToString();
            var jObject = JObject.Parse(json);

            var imageData = jObject["images"]?["photos"]?.FirstOrDefault();
             
            if (imageData == null)
            {
                return new ImageWidgetModel();
            }

            return new ImageWidgetModel
            {
                Id = (int)widgetResponseDTO.Widget.WidgetId,
                PhotographerName = imageData["photographer"]?.ToString(),
                PhotographerUrl = imageData["photographer_url"]?.ToString(),
                ImageAltText = imageData["alt"]?.ToString(),
                AvgColor = imageData["avg_color"]?.ToString(),
                OriginalUrl = imageData["src"]?["original"]?.ToString(),
                LargeUrl = imageData["src"]?["large"]?.ToString(),
                MediumUrl = imageData["src"]?["medium"]?.ToString(),
                SmallUrl = imageData["src"]?["small"]?.ToString(),
                PortraitUrl = imageData["src"]?["portrait"]?.ToString(),
                LandscapeUrl = imageData["src"]?["landscape"]?.ToString(),
                TinyUrl = imageData["src"]?["tiny"]?.ToString(),
                WidgetId = widgetResponseDTO.Widget.WidgetId
            };
        }
        public CityDetailsWidgetModel GetCityDetailsModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting)
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
            // Implementación personalizada para renderizar el widget de imagen.
            throw new NotImplementedException();
        }
    }
}
