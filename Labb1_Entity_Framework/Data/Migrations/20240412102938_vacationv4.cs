using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb1_Entity_Framework.Data.Migrations
{
    /// <inheritdoc />
    public partial class vacationv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FkUserId",
                table: "VacationRequest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FkUserId",
                table: "VacationRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
