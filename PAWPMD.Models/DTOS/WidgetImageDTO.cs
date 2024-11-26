using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Models.DTOS
{
public class WidgetImageDto
    {
        public int? Id { get; set; }
        public string? ImageUrl { get; set; }

        public string? ImageAltText { get; set; }

        public string? ImageTitle { get; set; }

        public string? Status { get; set; }

        public string? ThemeConfig { get; set; }

        public DateTime? LastModified { get; set; }

        public int? WidgetId { get; set; }

    }
}
