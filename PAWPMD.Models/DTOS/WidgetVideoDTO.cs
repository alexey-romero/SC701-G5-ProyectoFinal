using System;
using System.Collections.Generic;

namespace PAWPMD.Models.DTOS;

public partial class WidgetVideoDTO
{

    public int? Id { get; set; }
    public string? VideoUrl { get; set; }

    public string? VideoAltText { get; set; }

    public string? VideoTitle { get; set; }

    public string? Status { get; set; }

    public TimeOnly? Duration { get; set; }

    public string? ThemeConfig { get; set; }

    public DateTime? LastModified { get; set; }

    public int ? WidgetId { get; set; }


}
