using PAWPMD.Models;


namespace PAWPMD.Service.Mappers
{
    public static class WidgetMapper
    {
        public static Task<Widget> PrepareWidgetDataAsync(Widget widget, WidgetRequestDTO widgetRequestDTO, int? userId)
        {
            widget.Name = widgetRequestDTO.Widget.Name;
            widget.Description = widgetRequestDTO.Widget.Description;
            widget.Apiendpoint = widgetRequestDTO.Widget.Apiendpoint;
            widget.CategoryId = widgetRequestDTO.Widget.CategoryId;
            widget.RequiresApiKey = widgetRequestDTO.Widget.RequiresApiKey;
            widget.UserId = userId ?? widgetRequestDTO.Widget.UserId;
            widget.CreatedAt = DateTime.Now;

            return Task.FromResult(widget);
        }
    }
}
