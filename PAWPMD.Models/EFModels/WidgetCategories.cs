using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class WidgetCategories
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<WidgetCategory1> WidgetCategory1s { get; set; } = new List<WidgetCategory1>();
}
