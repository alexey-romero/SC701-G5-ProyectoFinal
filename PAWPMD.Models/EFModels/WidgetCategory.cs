using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class WidgetCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();
}
