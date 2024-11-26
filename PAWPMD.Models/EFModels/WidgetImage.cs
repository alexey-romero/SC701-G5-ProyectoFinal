using System;
using System.Collections.Generic;

namespace PAWPMD.Models;


public partial class WidgetImage : Widget
{
    public int Id { get; set; } 

    public string? ImageUrl { get; set; }
    public string? ImageAltText { get; set; }
    public string? ImageTitle { get; set; }
    public string? Status { get; set; }
    public string? ThemeConfig { get; set; }
    public DateTime? LastModified { get; set; }

    public int WidgetId { get; set; }  

    public virtual Widget Widget { get; set; } = null!;
}
