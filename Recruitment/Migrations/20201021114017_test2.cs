using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recruitment.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobProfileDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobProfileId = table.Column<long>(nullable: false),
                    JobElementId = table.Column<int>(nullable: false),
                    IsMandatory = table.Column<bool>(nullable: false),
                    MinRequirement = table.Column<string>(nullable: true),
                    MaxRequirement = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProfileDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobProfileDetails_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobProfileDetails_JobProfileElements_JobElementId",
                        column: x => x.JobElementId,
                        principalTable: "JobProfileElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobProfileDetails_JobProfiles_JobProfileId",
                        column: x => x.JobProfileId,
                        principalTable: "JobProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobProfileDetails_CreatedBy",
                table: "JobProfileDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfileDetails_JobElementId",
                table: "JobProfileDetails",
                column: "JobElementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfileDetails_JobProfileId",
                table: "JobProfileDetails",
                column: "JobProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobProfileDetails");
        }
    }
}
