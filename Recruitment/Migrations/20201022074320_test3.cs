using Microsoft.EntityFrameworkCore.Migrations;

namespace Recruitment.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JobProfileId",
                table: "JobRoles",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_JobRoles_JobProfileId",
                table: "JobRoles",
                column: "JobProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRoles_JobProfiles_JobProfileId",
                table: "JobRoles",
                column: "JobProfileId",
                principalTable: "JobProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRoles_JobProfiles_JobProfileId",
                table: "JobRoles");

            migrationBuilder.DropIndex(
                name: "IX_JobRoles_JobProfileId",
                table: "JobRoles");

            migrationBuilder.DropColumn(
                name: "JobProfileId",
                table: "JobRoles");
        }
    }
}
