using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Laklp.Platform.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_Entities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentaryResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: true),
                    MaintenanceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentaryResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentaryResources_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentaryResources_Maintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentaryResources_EntityId",
                table: "DocumentaryResources",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentaryResources_MaintenanceId",
                table: "DocumentaryResources",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ParentId",
                table: "Entities",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentaryResources");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "Maintenances");
        }
    }
}
