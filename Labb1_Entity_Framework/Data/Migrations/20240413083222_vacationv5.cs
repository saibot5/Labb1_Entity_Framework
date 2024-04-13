using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb1_Entity_Framework.Data.Migrations
{
    /// <inheritdoc />
    public partial class vacationv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "VacationRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "VacationRequest");
        }
    }
}
