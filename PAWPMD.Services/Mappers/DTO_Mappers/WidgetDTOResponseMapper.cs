using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using System.Linq;

namespace PAWPMD.Service.Mappers.DTOS;

    public static class WidgetDTOResponseMapper
    {
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

