namespace PAWPMD.Mvc.ViewStrategies
{
    public static class WidgetViewStrategyFactory
    {
        public static IWidgetViewStrategy GetStrategy(int categoryId)
        {
            return categoryId switch
            {
                2 => new WeatherWidgetViewStrategy(),
                _ => null
            };
        }
    }
}
