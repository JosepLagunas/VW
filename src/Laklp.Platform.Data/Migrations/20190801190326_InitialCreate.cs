using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Laklp.Platform.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "QualityAssurance");

            migrationBuilder.EnsureSchema(
                name: "Places");

            migrationBuilder.EnsureSchema(
                name: "Documentation");

            migrationBuilder.EnsureSchema(
                name: "HumanResources");

            migrationBuilder.EnsureSchema(
                name: "Miscellaneous");

            migrationBuilder.EnsureSchema(
                name: "Platform");

            migrationBuilder.CreateTable(
                name: "Geocoordinates",
                schema: "Miscellaneous",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geocoordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname1 = table.Column<string>(nullable: true),
                    Surname2 = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    GeocoordinateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entities_Geocoordinates_GeocoordinateId",
                        column: x => x.GeocoordinateId,
                        principalSchema: "Miscellaneous",
                        principalTable: "Geocoordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entities_Entities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CompanyLegalNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    GeocoordinateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Geocoordinates_GeocoordinateId",
                        column: x => x.GeocoordinateId,
                        principalSchema: "Miscellaneous",
                        principalTable: "Geocoordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Delegations",
                schema: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    DelegationType = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    GeocoordinateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delegations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delegations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Places",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delegations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delegations_Geocoordinates_GeocoordinateId",
                        column: x => x.GeocoordinateId,
                        principalSchema: "Miscellaneous",
                        principalTable: "Geocoordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HumanResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorksForId = table.Column<Guid>(nullable: true),
                    WorksAtId = table.Column<Guid>(nullable: true),
                    ResponsibleId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalSchema: "HumanResources",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Delegations_WorksAtId",
                        column: x => x.WorksAtId,
                        principalSchema: "Places",
                        principalTable: "Delegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_WorksForId",
                        column: x => x.WorksForId,
                        principalSchema: "Places",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceServices",
                schema: "QualityAssurance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DelegationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceServices_Delegations_DelegationId",
                        column: x => x.DelegationId,
                        principalSchema: "Places",
                        principalTable: "Delegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentaryResources",
                schema: "Documentation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: true),
                    MaintenanceServiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentaryResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentaryResources_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Places",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentaryResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentaryResources_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentaryResources_MaintenanceServices_MaintenanceServiceId",
                        column: x => x.MaintenanceServiceId,
                        principalSchema: "QualityAssurance",
                        principalTable: "MaintenanceServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interventions",
                schema: "QualityAssurance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    GeocoordinateId = table.Column<Guid>(nullable: true),
                    EvaluationStatus = table.Column<int>(nullable: false),
                    EvaluationResult = table.Column<int>(nullable: false),
                    DelegationId = table.Column<Guid>(nullable: true),
                    MaintenanceServiceId = table.Column<Guid>(nullable: true),
                    AssignedToId = table.Column<Guid>(nullable: true),
                    InterventionDescription = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interventions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interventions_Employees_AssignedToId",
                        column: x => x.AssignedToId,
                        principalSchema: "HumanResources",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interventions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interventions_Delegations_DelegationId",
                        column: x => x.DelegationId,
                        principalSchema: "Places",
                        principalTable: "Delegations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interventions_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interventions_Geocoordinates_GeocoordinateId",
                        column: x => x.GeocoordinateId,
                        principalSchema: "Miscellaneous",
                        principalTable: "Geocoordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interventions_MaintenanceServices_MaintenanceServiceId",
                        column: x => x.MaintenanceServiceId,
                        principalSchema: "QualityAssurance",
                        principalTable: "MaintenanceServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckPoints",
                schema: "QualityAssurance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ChallengeQuestion = table.Column<string>(nullable: true),
                    ChallengeType = table.Column<int>(nullable: false),
                    Satisfied = table.Column<bool>(nullable: false),
                    CheckPointStatus = table.Column<int>(nullable: false),
                    InterventionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckPoints_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Platform",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckPoints_Interventions_InterventionId",
                        column: x => x.InterventionId,
                        principalSchema: "QualityAssurance",
                        principalTable: "Interventions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_CreatedById",
                table: "Entities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_GeocoordinateId",
                table: "Entities",
                column: "GeocoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ParentId",
                table: "Entities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentaryResources_CompanyId",
                schema: "Documentation",
                table: "DocumentaryResources",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentaryResources_CreatedById",
                schema: "Documentation",
                table: "DocumentaryResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentaryResources_EntityId",
                schema: "Documentation",
                table: "DocumentaryResources",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentaryResources_MaintenanceServiceId",
                schema: "Documentation",
                table: "DocumentaryResources",
                column: "MaintenanceServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ResponsibleId",
                schema: "HumanResources",
                table: "Employees",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorksAtId",
                schema: "HumanResources",
                table: "Employees",
                column: "WorksAtId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorksForId",
                schema: "HumanResources",
                table: "Employees",
                column: "WorksForId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedById",
                schema: "Places",
                table: "Companies",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_GeocoordinateId",
                schema: "Places",
                table: "Companies",
                column: "GeocoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Delegations_CompanyId",
                schema: "Places",
                table: "Delegations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Delegations_CreatedById",
                schema: "Places",
                table: "Delegations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Delegations_GeocoordinateId",
                schema: "Places",
                table: "Delegations",
                column: "GeocoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedById",
                schema: "Platform",
                table: "Users",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_CreatedById",
                schema: "QualityAssurance",
                table: "CheckPoints",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_InterventionId",
                schema: "QualityAssurance",
                table: "CheckPoints",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_AssignedToId",
                schema: "QualityAssurance",
                table: "Interventions",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_CreatedById",
                schema: "QualityAssurance",
                table: "Interventions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_DelegationId",
                schema: "QualityAssurance",
                table: "Interventions",
                column: "DelegationId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_EntityId",
                schema: "QualityAssurance",
                table: "Interventions",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_GeocoordinateId",
                schema: "QualityAssurance",
                table: "Interventions",
                column: "GeocoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_MaintenanceServiceId",
                schema: "QualityAssurance",
                table: "Interventions",
                column: "MaintenanceServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceServices_CreatedById",
                schema: "QualityAssurance",
                table: "MaintenanceServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceServices_DelegationId",
                schema: "QualityAssurance",
                table: "MaintenanceServices",
                column: "DelegationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentaryResources",
                schema: "Documentation");

            migrationBuilder.DropTable(
                name: "CheckPoints",
                schema: "QualityAssurance");

            migrationBuilder.DropTable(
                name: "Interventions",
                schema: "QualityAssurance");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "MaintenanceServices",
                schema: "QualityAssurance");

            migrationBuilder.DropTable(
                name: "Delegations",
                schema: "Places");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Places");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Platform");

            migrationBuilder.DropTable(
                name: "Geocoordinates",
                schema: "Miscellaneous");
        }
    }
}
