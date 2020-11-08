using Microsoft.EntityFrameworkCore.Migrations;

namespace Recruitment.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRoles_JobProfiles_JobProfileId",
                table: "JobRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationJobRoles_OrganizationDepartments_DepartmentId",
                table: "OrganisationJobRoles");

            migrationBuilder.DropIndex(
                name: "IX_JobRoles_JobProfileId",
                table: "JobRoles");

            migrationBuilder.DropColumn(
                name: "JobProfileId",
                table: "JobRoles");

            migrationBuilder.AlterColumn<string>(
                name: "JobRoleName",
                table: "OrganisationJobRoles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "OrganisationJobRoles",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "JobProfileId",
                table: "OrganisationJobRoles",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationJobRoles_JobProfileId",
                table: "OrganisationJobRoles",
                column: "JobProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationJobRoles_OrganizationDepartments_DepartmentId",
                table: "OrganisationJobRoles",
                column: "DepartmentId",
                principalTable: "OrganizationDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationJobRoles_JobProfiles_JobProfileId",
                table: "OrganisationJobRoles",
                column: "JobProfileId",
                principalTable: "JobProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationJobRoles_OrganizationDepartments_DepartmentId",
                table: "OrganisationJobRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationJobRoles_JobProfiles_JobProfileId",
                table: "OrganisationJobRoles");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationJobRoles_JobProfileId",
                table: "OrganisationJobRoles");

            migrationBuilder.DropColumn(
                name: "JobProfileId",
                table: "OrganisationJobRoles");

            migrationBuilder.AlterColumn<string>(
                name: "JobRoleName",
                table: "OrganisationJobRoles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "OrganisationJobRoles",
                nullable: true,
                oldClrType: typeof(long));

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationJobRoles_OrganizationDepartments_DepartmentId",
                table: "OrganisationJobRoles",
                column: "DepartmentId",
                principalTable: "OrganizationDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
