using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamJourney.Migrations
{
    /// <inheritdoc />
    public partial class AddPeopleCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeopleCount",
                table: "TripApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleCount",
                table: "TripApplications");
        }
    }
}
