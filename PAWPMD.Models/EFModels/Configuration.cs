using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class Configuration
{
    public int ConfigurationId { get; set; }

    public int UserWidgetId { get; set; }

    public string? ApiKey { get; set; }

    public string? Settings { get; set; }

    public int? RefreshInterval { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual UserWidget UserWidget { get; set; } = null!;
}
