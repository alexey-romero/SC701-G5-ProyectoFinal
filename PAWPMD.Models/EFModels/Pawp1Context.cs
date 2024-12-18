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
        => optionsBuilder.UseSqlServer("Server=LAPTOP-22US8U5E\\SQLEXPRESS;Database=PAWP1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A9C3FC119");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F6473D75C5").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(18);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C91C7C313");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E40EF61677").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053472C659AB").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__User_Rol__3214EC07428F0104");

            entity.ToTable("User_Roles");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Role__RoleI__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Role__UserI__628FA481");
        });

        modelBuilder.Entity<UserWidget>(entity =>
        {
            entity.HasKey(e => e.UserWidgetId).HasName("PK__User_Wid__93DA71D2E77B0942");

            entity.ToTable("User_Widgets");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsFavorite).HasDefaultValue(false);
            entity.Property(e => e.IsVisible).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.UserWidgets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Widg__UserI__6383C8BA");

            entity.HasOne(d => d.Widget).WithMany(p => p.UserWidgets)
                .HasForeignKey(d => d.WidgetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Widg__Widge__6477ECF3");
        });

        modelBuilder.Entity<Widget>(entity =>
        {
            entity.HasKey(e => e.WidgetId).HasName("PK__Widgets__ADFD3012025B0735");

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
                .HasConstraintName("FK__Widgets__Categor__656C112C");
        });

        modelBuilder.Entity<WidgetCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Widget_C__19093A0BC0526FCD");

            entity.ToTable("Widget_Categories");

            entity.HasIndex(e => e.Name, "UQ__Widget_C__737584F675224700").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<WidgetSetting>(entity =>
        {
            entity.HasKey(e => e.WidgetSettingsId).HasName("PK__WidgetSe__C6BCB3C7F9C348FD");

            entity.HasOne(d => d.UserWidget).WithMany(p => p.WidgetSettings)
                .HasForeignKey(d => d.UserWidgetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WidgetSet__UserW__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
