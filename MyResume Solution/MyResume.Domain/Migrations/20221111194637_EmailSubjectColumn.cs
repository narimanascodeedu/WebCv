using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.Domain.Migrations
{
    public partial class EmailSubjectColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailSubject",
                table: "ContactPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSubject",
                table: "ContactPosts");
        }
    }
}
