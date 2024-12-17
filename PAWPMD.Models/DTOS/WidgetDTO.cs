using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Models.DTOS
{
public class WidgetDTO
    {
        public int? WidgetId { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Apiendpoint { get; set; } = null!;

        public bool? RequiresApiKey { get; set; }

        public int CategoryId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UserId { get; set; }

    }
}
