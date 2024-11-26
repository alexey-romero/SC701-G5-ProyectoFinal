using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWPMD.Models.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWidgetInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE1A10228A4C", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C92C0D2CD", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Widget_Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Widget_C__19093A0B586B8DB0", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Rol__3214EC071D5DB860", x => x.Id);
                    table.ForeignKey(
                        name: "FK__User_Role__RoleI__412EB0B6",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK__User_Role__UserI__403A8C7D",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Widgets",
                columns: table => new
                {
                    WidgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    APIEndpoint = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RequiresApiKey = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Widgets__ADFD30127DD2AE80", x => x.WidgetId);
                    table.ForeignKey(
                        name: "FK__Widgets__UserId__45F365D3",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "User_Widgets",
                columns: table => new
                {
                    UserWidgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Wid__93DA71D217BBE97A", x => x.UserWidgetId);
                    table.ForeignKey(
                        name: "FK__User_Widg__UserI__4BAC3F29",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__User_Widg__Widge__4CA06362",
                        column: x => x.WidgetId,
                        principalTable: "Widgets",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "Widget_Image",
                columns: table => new
                {
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageAltText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThemeConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Widgets__ADFD30127DD2AE80", x => x.WidgetId);
                    table.ForeignKey(
                        name: "FK_Widget_Image_Widgets",
                        column: x => x.WidgetId,
                        principalTable: "Widgets",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Widget_Table",
                columns: table => new
                {
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Headers = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Columns = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Rows = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThemeConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Widgets__ADFD30127DD2AE80", x => x.WidgetId);
                    table.ForeignKey(
                        name: "FK_Widget_Table_Widgets",
                        column: x => x.WidgetId,
                        principalTable: "Widgets",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Widget_Video",
                columns: table => new
                {
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VideoAltText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VideoTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: true),
                    ThemeConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Widgets__ADFD30127DD2AE80", x => x.WidgetId);
                    table.ForeignKey(
                        name: "FK_Widget_Video_Widgets",
                        column: x => x.WidgetId,
                        principalTable: "Widgets",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetCategories1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetCategories1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidgetCategories1_Widget_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Widget_Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WidgetCategories1_Widgets_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widgets",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetSettings",
                columns: table => new
                {
                    WidgetSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserWidgetId = table.Column<int>(type: "int", nullable: false),
                    Settings = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WidgetSe__C6BCB3C72DD79C30", x => x.WidgetSettingsId);
                    table.ForeignKey(
                        name: "FK__WidgetSet__UserW__6FE99F9F",
                        column: x => x.UserWidgetId,
                        principalTable: "User_Widgets",
                        principalColumn: "UserWidgetId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__737584F68DCFB241",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_RoleId",
                table: "User_Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_UserId",
                table: "User_Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Widgets_UserId",
                table: "User_Widgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Widgets_WidgetId",
                table: "User_Widgets",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E46065152A",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534CB0F9C61",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Widget_C__737584F676D6B6B4",
                table: "Widget_Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetCategories1_CategoryId",
                table: "WidgetCategories1",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetCategories1_WidgetId",
                table: "WidgetCategories1",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Widgets_UserId",
                table: "Widgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetSettings_UserWidgetId",
                table: "WidgetSettings",
                column: "UserWidgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "Widget_Image");

            migrationBuilder.DropTable(
                name: "Widget_Table");

            migrationBuilder.DropTable(
                name: "Widget_Video");

            migrationBuilder.DropTable(
                name: "WidgetCategories1");

            migrationBuilder.DropTable(
                name: "WidgetSettings");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Widget_Categories");

            migrationBuilder.DropTable(
                name: "User_Widgets");

            migrationBuilder.DropTable(
                name: "Widgets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
