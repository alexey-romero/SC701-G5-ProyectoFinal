using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class WidgetCategory1
{
    public int Id { get; set; }

    public int WidgetId { get; set; }

    public int CategoryId { get; set; }

    public virtual WidgetCategory Category { get; set; } = null!;

    public virtual Widget Widget { get; set; } = null!;
}
