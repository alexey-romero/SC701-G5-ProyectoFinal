using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class UserWidget
{
    public int UserWidgetId { get; set; }

    public int? UserId { get; set; }

    public int WidgetId { get; set; }

    public bool? IsFavorite { get; set; }

    public bool? IsVisible { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Widget Widget { get; set; } = null!;

    public virtual ICollection<WidgetSetting> WidgetSettings { get; set; } = new List<WidgetSetting>();
}
