using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Models.DTOS
{
    public class WidgetTableDTO
    {
        public int? Id { get; set; }
        public string? Headers { get; set; }

        public string? Columns { get; set; }

        public string? Rows { get; set; }

        public string? Status { get; set; }

        public string? ThemeConfig { get; set; }

        public DateTime? LastModified { get; set; }

        public int? WidgetId { get; set; }

    }
}
