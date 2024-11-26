using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAWPMD.Models;

using PAWPMD.Models.DTOS;
namespace PAWPMD.Service.Mappers
{
    public static class WidgetTableMapper
    {
        public static Task<WidgetTable> PrepareWidgetTableDataAsync(WidgetTable widgetTable, WidgetRequestDTO widgetRequestDTO)
        {

            widgetTable.Headers = widgetRequestDTO.Table.Headers;
            widgetTable.Rows = widgetRequestDTO.Table.Rows;
            widgetTable.Columns = widgetRequestDTO.Table.Columns;
            widgetTable.Status = widgetRequestDTO.Table.Status;
            widgetTable.ThemeConfig = widgetRequestDTO.Table.ThemeConfig;
            widgetTable.LastModified = DateTime.Now;

            return Task.FromResult(widgetTable);
        }
    }
}
