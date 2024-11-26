using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Mappers
{
    public static class WidgetImageMapper
    {
        public static Task<WidgetImage> PrepareWidgetTImageDataAsync(WidgetImage widgetImage, WidgetRequestDTO widgetRequestDTO)
        {
            widgetImage.ImageUrl = widgetRequestDTO.Image.ImageUrl;
            widgetImage.ImageAltText = widgetRequestDTO.Image.ImageAltText;
            widgetImage.ImageTitle = widgetRequestDTO.Image.ImageTitle;
            widgetImage.Status = widgetRequestDTO.Image.Status;
            widgetImage.ThemeConfig = widgetRequestDTO.Image.ThemeConfig;
            widgetImage.LastModified = DateTime.Now;

            return Task.FromResult(widgetImage);
        }
    }

}
