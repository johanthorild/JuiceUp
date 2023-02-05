using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialwithseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargerSpeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kilowatt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargerSpeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OpenTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CloseTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FailedLogins = table.Column<byte>(type: "tinyint", nullable: false),
                    LockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chargers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    ChargerSpeedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chargers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chargers_ChargerSpeeds_ChargerSpeedId",
                        column: x => x.ChargerSpeedId,
                        principalTable: "ChargerSpeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chargers_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCars", x => new { x.UserId, x.CarId });
                    table.ForeignKey(
                        name: "FK_UserCars_CarModels_CarId",
                        column: x => x.CarId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ChargerId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => new { x.UserId, x.ChargerId });
                    table.ForeignKey(
                        name: "FK_Reservations_Chargers_ChargerId",
                        column: x => x.ChargerId,
                        principalTable: "Chargers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "Capacity", "LastChanged", "LastChangedBy", "Name" },
                values: new object[,]
                {
                    { 1, 57.5, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Tesla Model 3" },
                    { 2, 54.0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Kia EV6" },
                    { 3, 51.0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "MG 4" },
                    { 4, 45.0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Opel Corsa-e" }
                });

            migrationBuilder.InsertData(
                table: "ChargerSpeeds",
                columns: new[] { "Id", "Kilowatt" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 2, 22 },
                    { 3, 50 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "LastChanged", "LastChangedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Reader" },
                    { 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "Address", "City", "CloseTime", "LastChanged", "LastChangedBy", "Name", "OpenTime", "ZipCode" },
                values: new object[,]
                {
                    { 1, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Stockholm", null, null },
                    { 2, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Linköping", null, null },
                    { 3, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Jönköping", null, null },
                    { 4, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Ljungby", null, null },
                    { 5, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DataSeed", "Halmstad", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chargers_ChargerSpeedId",
                table: "Chargers",
                column: "ChargerSpeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Chargers_Id",
                table: "Chargers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chargers_StationId",
                table: "Chargers",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ChargerId",
                table: "Reservations",
                column: "ChargerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Id",
                table: "Reservations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_Id",
                table: "Stations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCars_CarId",
                table: "UserCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "UserCars");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Chargers");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ChargerSpeeds");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
