using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Models.DTOS
{
    public class UserWidgetDTO
    {
        public int UserWidgetId { get; set; }
        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public bool? IsFavorite { get; set; }
        public bool? IsVisible { get; set; }


    }
}
