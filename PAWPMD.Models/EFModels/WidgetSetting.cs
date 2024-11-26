using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class WidgetSetting
{
    public int WidgetSettingsId { get; set; }

    public int UserWidgetId { get; set; }

    public string? Settings { get; set; }

    public virtual UserWidget UserWidget { get; set; } = null!;
}
