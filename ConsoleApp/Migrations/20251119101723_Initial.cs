using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    tpep_pickup_datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tpep_dropoff_datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    seconds_travelled = table.Column<int>(type: "int", nullable: true),
                    passenger_count = table.Column<int>(type: "int", nullable: true),
                    trip_distance = table.Column<double>(type: "float", nullable: true),
                    store_and_fwd_flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PULocationID = table.Column<int>(type: "int", nullable: true),
                    DOLocationID = table.Column<int>(type: "int", nullable: true),
                    fare_amount = table.Column<double>(type: "float", nullable: true),
                    tip_amount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rides_PULocationID",
                table: "Rides",
                column: "PULocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_seconds_travelled",
                table: "Rides",
                column: "seconds_travelled");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_trip_distance",
                table: "Rides",
                column: "trip_distance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rides");
        }
    }
}
