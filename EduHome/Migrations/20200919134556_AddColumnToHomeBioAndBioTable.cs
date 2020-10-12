using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class AddColumnToHomeBioAndBioTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "HomeBios");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Bios");

            migrationBuilder.AddColumn<string>(
                name: "FooterLogo",
                table: "HomeBios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderLogo",
                table: "HomeBios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterLogo",
                table: "Bios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderLogo",
                table: "Bios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterLogo",
                table: "HomeBios");

            migrationBuilder.DropColumn(
                name: "HeaderLogo",
                table: "HomeBios");

            migrationBuilder.DropColumn(
                name: "FooterLogo",
                table: "Bios");

            migrationBuilder.DropColumn(
                name: "HeaderLogo",
                table: "Bios");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "HomeBios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Bios",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
