namespace PAWPMD.Mvc.Models
{
    public class ImageWidgetModel
    {
        public int Id { get; set; }
        public string PhotographerName { get; set; }
        public string PhotographerUrl { get; set; }
        public string ImageAltText { get; set; }
        public string AvgColor { get; set; }
        public string OriginalUrl { get; set; }
        public string LargeUrl { get; set; }
        public string MediumUrl { get; set; }
        public string SmallUrl { get; set; }
        public string PortraitUrl { get; set; }
        public string LandscapeUrl { get; set; }
        public string TinyUrl { get; set; }
        public int? WidgetId { get; set; }
    }
}
