using Microsoft.EntityFrameworkCore.Migrations;

namespace Laklp.Platform.Data.Migrations
{
    public partial class Update_Resources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                schema: "Documentation",
                table: "DocumentaryResources",
                newName: "S3Url");

            migrationBuilder.AddColumn<string>(
                name: "S3Path",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "S3Region",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "S3Path",
                schema: "Documentation",
                table: "DocumentaryResources");

            migrationBuilder.DropColumn(
                name: "S3Region",
                schema: "Documentation",
                table: "DocumentaryResources");

            migrationBuilder.RenameColumn(
                name: "S3Url",
                schema: "Documentation",
                table: "DocumentaryResources",
                newName: "Url");
        }
    }
}
