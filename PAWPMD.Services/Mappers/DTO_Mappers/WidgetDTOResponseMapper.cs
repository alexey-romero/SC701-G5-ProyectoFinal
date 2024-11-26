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

                widgetResponseDTO.Widget.Name = widget.Name;
                widgetResponseDTO.Widget.Description = widget.Description;
                widgetResponseDTO.Widget.Apiendpoint = widget.Apiendpoint;
                widgetResponseDTO.Widget.RequiresApiKey = widget.RequiresApiKey;
                widgetResponseDTO.Widget.CreatedAt = widget.CreatedAt;
                widgetResponseDTO.Widget.UserId = widget.UserId;

            }

            if (userWidget != null) { 
               widgetResponseDTO.UserWidget.UserWidgetId = userWidget.UserWidgetId;
               widgetResponseDTO.UserWidget.PositionX = userWidget.PositionX;
               widgetResponseDTO.UserWidget.PositionY = userWidget.PositionY;
               widgetResponseDTO.UserWidget.IsFavorite = userWidget.IsFavorite;
               widgetResponseDTO.UserWidget.IsVisible = userWidget.IsVisible;
            }

            if (widgetSetting != null)
            {
                widgetResponseDTO.WidgetSetting.WidgetSettingsId = widgetSetting.WidgetSettingsId;
                widgetResponseDTO.WidgetSetting.Settings = widgetSetting.Settings;
            }

            if (widget is WidgetVideo widgetVideo)
            {
                widgetResponseDTO.Type = "VideoWidget";
                widgetResponseDTO.Video.Id = widgetVideo.Id;
                widgetResponseDTO.Video.VideoUrl = widgetVideo.VideoUrl;
                widgetResponseDTO.Video.VideoAltText = widgetVideo.VideoAltText;
                widgetResponseDTO.Video.VideoTitle = widgetVideo.VideoTitle;
                widgetResponseDTO.Video.Status = widgetVideo.Status;
                widgetResponseDTO.Video.Duration = widgetVideo.Duration;
                widgetResponseDTO.Video.ThemeConfig = widgetVideo.ThemeConfig;
                widgetResponseDTO.Video.WidgetId = widget.WidgetId;
                widgetResponseDTO.Video.LastModified = widgetVideo.LastModified;
            }

            if (widget is WidgetImage widgetImage)
            {
                widgetResponseDTO.Type = "ImageWidget";
                widgetResponseDTO.Image.Id = widgetImage.Id;
                widgetResponseDTO.Image.ImageUrl = widgetImage.ImageUrl;
                widgetResponseDTO.Image.ImageAltText = widgetImage.ImageAltText;
                widgetResponseDTO.Image.ImageTitle = widgetImage.ImageTitle;
                widgetResponseDTO.Image.Status = widgetImage.Status;
                widgetResponseDTO.Image.ThemeConfig = widgetImage.ThemeConfig;
                widgetResponseDTO.Image.LastModified = widgetImage.LastModified;
                widgetResponseDTO.Image.WidgetId = widget.WidgetId;
            }

            if (widget is WidgetTable widgetTable)
            {
                widgetResponseDTO.Type = "TableWidget";
                widgetResponseDTO.Table.Id = widgetTable.Id;
                widgetResponseDTO.Table.Columns = widgetTable.Columns;
                widgetResponseDTO.Table.Rows = widgetTable.Rows;
                widgetResponseDTO.Table.Headers = widgetTable.Headers;
                widgetResponseDTO.Table.Status = widgetTable.Status;
                widgetResponseDTO.Table.LastModified = widgetTable.LastModified;
                widgetResponseDTO.Table.WidgetId = widget.WidgetId;
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
                Video = new WidgetVideoDTO(),
                Image = new WidgetImageDto(),
                Table = new WidgetTableDTO(),
                UserWidget = new UserWidgetDTO(),
                WidgetSetting = new WidgetSettingDTO()
            };

            // Preparar los datos del DTO usando el método principal
            await PrepareWidgetDTOReponseDataAsync(widgetResponseDTO, widget, userWidget, widgetSetting);

            widgetResponseDTOs.Add(widgetResponseDTO);
        }

        return widgetResponseDTOs;
    }
}

