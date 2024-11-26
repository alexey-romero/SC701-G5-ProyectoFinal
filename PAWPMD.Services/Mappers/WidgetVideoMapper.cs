using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Mappers
{
    public static class WidgetVideoMapper
    {

        public static Task<WidgetVideo> PrepareWidgetVideoDataAsync(WidgetVideo widgetVideo, WidgetRequestDTO widgetRequestDTO)
        {
     
            widgetVideo.VideoUrl = widgetRequestDTO.Video.VideoUrl;
            widgetVideo.VideoAltText = widgetRequestDTO.Video.VideoAltText;
            widgetVideo.VideoTitle = widgetRequestDTO.Video.VideoTitle;
            widgetVideo.Status = widgetRequestDTO.Video.Status;
            widgetVideo.Duration = widgetRequestDTO.Video.Duration;
            widgetVideo.ThemeConfig = widgetRequestDTO.Video.ThemeConfig;
            widgetVideo.LastModified = DateTime.Now;

            return Task.FromResult(widgetVideo);
        }
    }
}
