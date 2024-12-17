using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.ViewStrategies
{
    public interface IWidgetViewStrategy
   {
       public void RenderWidget(WidgetResponseDTO widgetResponseDTO, WidgetSetting widgetSetting);
        WeatherWidgetModel GetWeatherModel(WidgetResponseDTO widgetResponseDTO, WidgetSetting setting);
    }
}
