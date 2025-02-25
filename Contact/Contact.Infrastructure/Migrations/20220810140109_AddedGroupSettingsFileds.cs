using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact.Infrastructure.Migrations
{
    public partial class AddedGroupSettingsFileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutoRegisterCancelKeyWord",
                table: "GroupSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutoRegisterCancelLineNumber",
                table: "GroupSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutoRegisterKeyWord",
                table: "GroupSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutoRegisterLineNumber",
                table: "GroupSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoRegisterCancelKeyWord",
                table: "GroupSettings");

            migrationBuilder.DropColumn(
                name: "AutoRegisterCancelLineNumber",
                table: "GroupSettings");

            migrationBuilder.DropColumn(
                name: "AutoRegisterKeyWord",
                table: "GroupSettings");

            migrationBuilder.DropColumn(
                name: "AutoRegisterLineNumber",
                table: "GroupSettings");
        }
    }
}
