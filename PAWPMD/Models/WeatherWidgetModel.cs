namespace PAWPMD.Mvc.Models
{
    public class WeatherWidgetModel
    {
        public string Location { get; set; } 
        public double CurrentTemperature { get; set; } 
        public string WeatherCondition { get; set; } 
        public string WeatherIcon { get; set; }
        public double MaxTemperature { get; set; } 
        public double MinTemperature { get; set; } 
        public int Humidity { get; set; } 
        public int PrecipitationProbability { get; set; }
        public int? WidgetId { get; set; }
    }
}
