namespace PAWPMD.Mvc.Models
{
    public class WidgetFormModel
    {
        public string WidgetName { get; set; }
        public string WidgetDescription { get; set; }
        public int CategoryId { get; set; }
        public bool favorite { get; set; }
        public bool visible { get; set; }
        public WidgetApiModel Widget { get; set; }
        public Dictionary<string, string> WidgetSetting { get; set; }
    }
}
