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

    public int CategoryId { get; set; }

    public virtual WidgetCategory Category { get; set; } = null!;

    public virtual ICollection<UserWidget> UserWidgets { get; set; } = new List<UserWidget>();
}
