using System;
using System.Collections.Generic;

namespace PAWPMD.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? SecondLastName { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<UserWidget> UserWidgets { get; set; } = new List<UserWidget>();

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();
}
