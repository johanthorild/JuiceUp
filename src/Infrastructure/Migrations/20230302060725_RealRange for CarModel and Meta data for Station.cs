using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RealRangeforCarModelandMetadataforStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasConference",
                table: "Stations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPersonel",
                table: "Stations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasRestaurant",
                table: "Stations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RealRange",
                table: "CarModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1,
                column: "RealRange",
                value: 380.0);

            migrationBuilder.UpdateData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 2,
                column: "RealRange",
                value: 305.0);

            migrationBuilder.UpdateData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 3,
                column: "RealRange",
                value: 300.0);

            migrationBuilder.UpdateData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 4,
                column: "RealRange",
                value: 285.0);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasConference", "HasPersonel", "HasRestaurant" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasConference", "HasPersonel", "HasRestaurant" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasConference", "HasPersonel", "HasRestaurant" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasConference", "HasPersonel", "HasRestaurant" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HasConference", "HasPersonel", "HasRestaurant" },
                values: new object[] { null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasConference",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "HasPersonel",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "HasRestaurant",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "RealRange",
                table: "CarModels");
        }
    }
}
