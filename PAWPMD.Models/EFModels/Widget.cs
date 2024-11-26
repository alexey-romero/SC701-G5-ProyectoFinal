using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class Widget
{
    public int WidgetId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Apiendpoint { get; set; } = null!;

    public bool? RequiresApiKey { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<UserWidget> UserWidgets { get; set; } = new List<UserWidget>();

    public virtual ICollection<WidgetCategory1> WidgetCategory1s { get; set; } = new List<WidgetCategory1>();

    public virtual ICollection<WidgetImage> WidgetImages { get; set; } = new List<WidgetImage>();

    public virtual ICollection<WidgetTable> WidgetTables { get; set; } = new List<WidgetTable>();

    public virtual ICollection<WidgetVideo> WidgetVideos { get; set; } = new List<WidgetVideo>();
}
