using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class UserWidget
{
    public int UserWidgetId { get; set; }

    public int UserId { get; set; }

    public int WidgetId { get; set; }

    public int PositionX { get; set; }

    public int PositionY { get; set; }

    public bool? IsFavorite { get; set; }

    public bool? IsVisible { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Configuration> Configurations { get; set; } = new List<Configuration>();

    public virtual User User { get; set; } = null!;

    public virtual Widget Widget { get; set; } = null!;
}
