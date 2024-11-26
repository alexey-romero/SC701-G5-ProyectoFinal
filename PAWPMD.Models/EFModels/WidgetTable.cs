using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class WidgetTable: Widget
{
    public int Id { get; set; }

    public string? Headers { get; set; }

    public string? Columns { get; set; }

    public string? Rows { get; set; }

    public string? Status { get; set; }

    public string? ThemeConfig { get; set; }

    public DateTime? LastModified { get; set; }

    public int WidgetId { get; set; }

    public virtual Widget Widget { get; set; } = null!;
}
