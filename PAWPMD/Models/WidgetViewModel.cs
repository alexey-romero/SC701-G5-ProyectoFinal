using PAWPMD.Models;

namespace PAWPMD.Mvc.Models
{
    public class WidgetViewModel
    {
        public List<Widget> Widgets { get; set; }
        public List<WidgetSetting> WidgetSettings { get; set; }

        public List<WeatherWidgetModel>? WeatherWidgets { get; set; }

    }
}
