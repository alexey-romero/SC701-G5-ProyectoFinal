using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using System.Linq;

namespace PAWPMD.Service.Mappers.DTOS;

    public static class WidgetDTOResponseMapper
    {
    /// <summary>
    /// Asynchronously prepares a `WidgetResponseDTO` by populating its properties with data from the provided `Widget`, `UserWidget`, and `WidgetSetting` objects.
    /// The method updates the `Widget`, `UserWidget`, and `WidgetSetting` properties of the `WidgetResponseDTO` with the corresponding data from the input objects.
    /// </summary>
    /// <param name="widgetResponseDTO">The `WidgetResponseDTO` to populate with data.</param>
    /// <param name="widget">The `Widget` object containing the widget data to be included in the response.</param>
    /// <param name="userWidget">The `UserWidget` object containing user-specific widget data to be included in the response.</param>
    /// <param name="widgetSetting">The `WidgetSetting` object containing the settings for the widget to be included in the response.</param>
    /// <returns>A `Task` representing the asynchronous operation. The task result is the populated `WidgetResponseDTO` object.</returns>

    public static async Task<WidgetResponseDTO> PrepareWidgetDTOReponseDataAsync(
         WidgetResponseDTO widgetResponseDTO, Widget widget, UserWidget userWidget, WidgetSetting widgetSetting)
        {
            if (widget != null)
            {
                widgetResponseDTO.Widget.WidgetId = widget.WidgetId;
                widgetResponseDTO.Widget.Name = widget.Name;
                widgetResponseDTO.Widget.Description = widget.Description;
                widgetResponseDTO.Widget.Apiendpoint = widget.Apiendpoint;
                widgetResponseDTO.Widget.RequiresApiKey = widget.RequiresApiKey;
                widgetResponseDTO.Widget.CategoryId = widget.CategoryId;
                widgetResponseDTO.Widget.CreatedAt = widget.CreatedAt;
                widgetResponseDTO.Widget.UserId = widget.UserId;

            }

            if (userWidget != null) { 
               widgetResponseDTO.UserWidget.UserWidgetId = userWidget.UserWidgetId;
               widgetResponseDTO.UserWidget.IsFavorite = userWidget.IsFavorite;
               widgetResponseDTO.UserWidget.IsVisible = userWidget.IsVisible;
            }

            if (widgetSetting != null)
            {
                widgetResponseDTO.WidgetSetting.WidgetSettingsId = widgetSetting.WidgetSettingsId;
                widgetResponseDTO.WidgetSetting.Settings = widgetSetting.Settings;
            }
            await Task.CompletedTask;

            return widgetResponseDTO;
        }


    /// <summary>
    /// Asynchronously prepares a list of `WidgetResponseDTO` objects by populating each DTO with data from the provided collections of `Widget`, `UserWidget`, and `WidgetSetting` objects.
    /// The method iterates through the list of `Widget` objects, finds the corresponding `UserWidget` and `WidgetSetting` for each `Widget`, and then uses the `PrepareWidgetDTOReponseDataAsync` method to populate the `WidgetResponseDTO` with the relevant data.
    /// </summary>
    /// <param name="widgets">An enumerable collection of `Widget` objects to be included in the response DTOs.</param>
    /// <param name="userWidgets">An enumerable collection of `UserWidget` objects that provide user-specific information related to the widgets.</param>
    /// <param name="widgetSettings">An enumerable collection of `WidgetSetting` objects that provide settings related to the widgets.</param>
    /// <returns>A `Task` representing the asynchronous operation. The task result is a list of populated `WidgetResponseDTO` objects.</returns>
    public static async Task<List<WidgetResponseDTO>> PrepareWidgetDTOResponseDataListAsync(
    IEnumerable<Widget> widgets,
    IEnumerable<UserWidget> userWidgets,
    IEnumerable<WidgetSetting> widgetSettings)
    {
        var widgetResponseDTOs = new List<WidgetResponseDTO>();

        foreach (var widget in widgets)
        {
            var userWidget = userWidgets.FirstOrDefault(uw => uw.WidgetId == widget.WidgetId);
            var widgetSetting = widgetSettings.FirstOrDefault(ws => ws.UserWidgetId == userWidget.UserWidgetId);

            var widgetResponseDTO = new WidgetResponseDTO
            {
                Widget = new WidgetDTO(),
                UserWidget = new UserWidgetDTO(),
                WidgetSetting = new WidgetSettingDTO()
            };

            
            await PrepareWidgetDTOReponseDataAsync(widgetResponseDTO, widget, userWidget, widgetSetting);

            widgetResponseDTOs.Add(widgetResponseDTO);
        }

        return widgetResponseDTOs;
    }

 
}

