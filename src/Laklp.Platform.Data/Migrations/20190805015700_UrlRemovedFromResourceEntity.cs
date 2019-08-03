using Microsoft.EntityFrameworkCore.Migrations;

namespace Laklp.Platform.Data.Migrations
{
    public partial class UrlRemovedFromResourceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "S3Url",
                schema: "Documentation",
                table: "DocumentaryResources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "S3Url",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: true);
        }
    }
}
