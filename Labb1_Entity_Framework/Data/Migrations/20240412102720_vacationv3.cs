using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb1_Entity_Framework.Data.Migrations
{
    /// <inheritdoc />
    public partial class vacationv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequest_AspNetUsers_EmployeeId",
                table: "VacationRequest");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "VacationRequest",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FkUserId",
                table: "VacationRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequest_AspNetUsers_EmployeeId",
                table: "VacationRequest",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequest_AspNetUsers_EmployeeId",
                table: "VacationRequest");

            migrationBuilder.DropColumn(
                name: "FkUserId",
                table: "VacationRequest");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "VacationRequest",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequest_AspNetUsers_EmployeeId",
                table: "VacationRequest",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
