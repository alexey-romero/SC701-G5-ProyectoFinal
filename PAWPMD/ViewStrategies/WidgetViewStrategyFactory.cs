namespace PAWPMD.Mvc.ViewStrategies
{
    public static class WidgetViewStrategyFactory
    {
        public static IWidgetViewStrategy GetStrategy(int categoryId)
        {
            return categoryId switch
            {

                1 => new ImageWidgetViewStrategy(),
                2 => new WeatherWidgetViewStrategy(),
                3 => new CityDetailsWidgetViewStrategy(),
                4 => new NewsWidgetViewStrategy(),

                _ => null
            };
        }
    }
}
