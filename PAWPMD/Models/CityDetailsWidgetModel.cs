namespace PAWPMD.Mvc.Models
{
    public class CityDetailsWidgetModel
    {
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public bool IsCapital { get; set; }
        public int? WidgetId { get; set; }
    }
}
