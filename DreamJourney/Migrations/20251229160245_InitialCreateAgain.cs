using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DreamJourney.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SingleSelection = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Role = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FeatureCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_FeatureCategories_FeatureCategoryId",
                        column: x => x.FeatureCategoryId,
                        principalTable: "FeatureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Destinations = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    FromPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripApplications_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripApplications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripFeatures",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripFeatures", x => new { x.TripId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_TripFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripFeatures_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Екскурзии" },
                    { 2, "Почивки" },
                    { 3, "Приключения" }
                });

            migrationBuilder.InsertData(
                table: "FeatureCategories",
                columns: new[] { "Id", "Name", "SingleSelection" },
                values: new object[,]
                {
                    { 1, "Транспорт", false },
                    { 2, "Хотел", false },
                    { 3, "Хранене", false },
                    { 4, "Екстри", false }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Градски турове" },
                    { 2, 1, "Културни екскурзии" },
                    { 3, 2, "Морски почивки" },
                    { 4, 2, "Планински почивки" },
                    { 5, 3, "Екстремни" },
                    { 6, 3, "Еко туризъм" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "FeatureCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Самолет" },
                    { 2, 1, "Автобус" },
                    { 3, 2, "Хотел 2*" },
                    { 4, 2, "Хотел 3*" },
                    { 5, 2, "Хотел 4*" },
                    { 6, 2, "Хотел 5*" },
                    { 7, 3, "Закуска" },
                    { 8, 3, "HB" },
                    { 9, 3, "FB" },
                    { 10, 3, "All Inclusive" },
                    { 11, 4, "Wi-Fi" },
                    { 12, 4, "Трансфер" },
                    { 13, 4, "Басейн" },
                    { 14, 4, "Аквапарк" },
                    { 15, 4, "Фитнес" },
                    { 16, 4, "СПА" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Уикенд" },
                    { 2, 1, "Седмични" },
                    { 3, 2, "Исторически" },
                    { 4, 2, "Музеи" },
                    { 5, 3, "Лято" },
                    { 6, 4, "Зима" },
                    { 7, 5, "Рафтинг" },
                    { 8, 6, "Пешеходни" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DepartmentId",
                table: "Categories",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_FeatureCategoryId",
                table: "Features",
                column: "FeatureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TripApplications_TripId",
                table: "TripApplications",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripApplications_UserId",
                table: "TripApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripFeatures_FeatureId",
                table: "TripFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CategoryId",
                table: "Trips",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DepartmentId",
                table: "Trips",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SubCategoryId",
                table: "Trips",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripApplications");

            migrationBuilder.DropTable(
                name: "TripFeatures");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "FeatureCategories");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
