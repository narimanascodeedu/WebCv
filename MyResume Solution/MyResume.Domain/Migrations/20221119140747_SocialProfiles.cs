using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.Domain.Migrations
{
    public partial class SocialProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GitHubLink",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinLink",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterLink",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeLink",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "GitHubLink",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "LinkedinLink",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "TwitterLink",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "YoutubeLink",
                table: "Abouts");
        }
    }
}
