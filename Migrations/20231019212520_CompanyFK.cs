using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEX_backend.Migrations
{
    /// <inheritdoc />
    public partial class CompanyFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpenings_Companies_CompanyId",
                table: "JobOpenings");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "JobOpenings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpenings_Companies_CompanyId",
                table: "JobOpenings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpenings_Companies_CompanyId",
                table: "JobOpenings");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "JobOpenings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpenings_Companies_CompanyId",
                table: "JobOpenings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
