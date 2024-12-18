using PAWPMD.Models;


namespace PAWPMD.Service.Mappers
{
    public static class WidgetMapper
    {

        /// <summary>
        /// Prepares and returns a `Widget` object asynchronously by updating its properties based on the provided `WidgetRequestDTO`.
        /// The method updates various properties of the `Widget` such as `WidgetId`, `Name`, `Description`, `Apiendpoint`, `CategoryId`, and `RequiresApiKey`.
        /// The `UserId` is set from the request or the existing `UserId`, and the creation timestamp is set to the current time.
        /// </summary>
        /// <param name="widget">The `Widget` object to update with new values from the `widgetRequestDTO`.</param>
        /// <param name="widgetRequestDTO">The `WidgetRequestDTO` containing the new values for the widget.</param>
        /// <param name="userId">The user ID to associate with the widget. If null, the `UserId` from `widgetRequestDTO` is used.</param>
        /// <returns>A `Task` that represents the asynchronous operation. The task result is the updated `Widget` object.</returns>

        public static Task<Widget> PrepareWidgetDataAsync(Widget widget, WidgetRequestDTO widgetRequestDTO, int? userId)
        {
            widget.WidgetId = widgetRequestDTO.Widget.WidgetId;
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
