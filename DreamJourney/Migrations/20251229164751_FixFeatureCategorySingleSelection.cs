using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamJourney.Migrations
{
    /// <inheritdoc />
    public partial class FixFeatureCategorySingleSelection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FeatureCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "SingleSelection",
                value: true);

            migrationBuilder.UpdateData(
                table: "FeatureCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "SingleSelection",
                value: true);

            migrationBuilder.UpdateData(
                table: "FeatureCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "SingleSelection",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FeatureCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "SingleSelection",
                value: false);

            migrationBuilder.UpdateData(
                table: "FeatureCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "SingleSelection",
                value: false);

            migrationBuilder.UpdateData(
                table: "FeatureCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "SingleSelection",
                value: false);
        }
    }
}
