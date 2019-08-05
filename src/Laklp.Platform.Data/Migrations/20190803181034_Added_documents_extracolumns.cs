using Microsoft.EntityFrameworkCore.Migrations;

namespace Laklp.Platform.Data.Migrations
{
    public partial class Added_documents_extracolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "S3Bucket",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "S3Key",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                schema: "Documentation",
                table: "DocumentaryResources");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                schema: "Documentation",
                table: "DocumentaryResources");

            migrationBuilder.DropColumn(
                name: "S3Bucket",
                schema: "Documentation",
                table: "DocumentaryResources");

            migrationBuilder.DropColumn(
                name: "S3Key",
                schema: "Documentation",
                table: "DocumentaryResources");
        }
    }
}
