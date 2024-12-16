using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PAWPMD.Models;
public partial class Pawp1Context : DbContext
{
    public Pawp1Context()
    {
    }

    public Pawp1Context(DbContextOptions<Pawp1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserWidget> UserWidgets { get; set; }

    public virtual DbSet<Widget> Widgets { get; set; }

    public virtual DbSet<WidgetCategory> WidgetCategories { get; set; }

    public virtual DbSet<WidgetSetting> WidgetSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-N37CUPH;Database=PAWP1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A10228A4C");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F68DCFB241").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(18);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C92C0D2CD");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E46065152A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534CB0F9C61").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.SecondLastName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(18);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_Rol__3214EC071D5DB860");

            entity.ToTable("User_Roles");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Role__RoleI__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Role__UserI__403A8C7D");
        });

        modelBuilder.Entity<UserWidget>(entity =>
        {
            entity.HasKey(e => e.UserWidgetId).HasName("PK__User_Wid__93DA71D2A9460BC6");

            entity.ToTable("User_Widgets");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsFavorite).HasDefaultValue(false);
            entity.Property(e => e.IsVisible).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.UserWidgets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Widg__UserI__45BE5BA9");

            entity.HasOne(d => d.Widget).WithMany(p => p.UserWidgets)
                .HasForeignKey(d => d.WidgetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Widg__Widge__46B27FE2");
        });

        modelBuilder.Entity<Widget>(entity =>
        {
            entity.HasKey(e => e.WidgetId).HasName("PK__Widgets__ADFD301286D17E50");

            entity.Property(e => e.Apiendpoint)
                .HasMaxLength(255)
                .HasColumnName("APIEndpoint");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RequiresApiKey).HasDefaultValue(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Widgets)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Widgets__Categor__40058253");
        });

        modelBuilder.Entity<WidgetCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Widget_C__19093A0B986DEF39");

            entity.ToTable("Widget_Categories");

            entity.HasIndex(e => e.Name, "UQ__Widget_C__737584F646B370AF").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<WidgetSetting>(entity =>
        {
            entity.HasKey(e => e.WidgetSettingsId).HasName("PK__WidgetSe__C6BCB3C7F2B9041F");

            entity.HasOne(d => d.UserWidget).WithMany(p => p.WidgetSettings)
                .HasForeignKey(d => d.UserWidgetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WidgetSet__UserW__498EEC8D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
