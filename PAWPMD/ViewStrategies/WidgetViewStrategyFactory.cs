using PAWPMD.Models.Enums;

namespace PAWPMD.Mvc.ViewStrategies
{
    public static class WidgetViewStrategyFactory
    {
        public static IWidgetViewStrategy GetStrategy(WidgetType widgetType)
        {
            return widgetType switch
            {
                WidgetType.Image => new ImageWidgetViewStrategy(),
                WidgetType.Weather => new WeatherWidgetViewStrategy(),
                WidgetType.CityDetails => new CityDetailsWidgetViewStrategy(),
                WidgetType.News => new NewsWidgetViewStrategy(),
                _ => throw new ArgumentException($"No strategy found for widget type: {widgetType}")
            };
        }
    }
}
