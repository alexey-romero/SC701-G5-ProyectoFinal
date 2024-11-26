using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Models.DTOS
{
    public class WidgetSettingDTO
    {
        public int WidgetSettingsId { get; set; }
        public int UserWidgetId { get; set; }
        public string? Settings { get; set; }

    }
}
