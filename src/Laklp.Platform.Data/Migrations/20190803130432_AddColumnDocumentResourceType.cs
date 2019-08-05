using Microsoft.EntityFrameworkCore.Migrations;

namespace Laklp.Platform.Data.Migrations
{
    public partial class AddColumnDocumentResourceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentaryResourceType",
                schema: "Documentation",
                table: "DocumentaryResources",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentaryResourceType",
                schema: "Documentation",
                table: "DocumentaryResources");
        }
    }
}
