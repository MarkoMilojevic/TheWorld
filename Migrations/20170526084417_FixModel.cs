using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapp.Migrations
{
    public partial class FixModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stop_Trip_TripId",
                table: "Stop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stop",
                table: "Stop");

            migrationBuilder.RenameTable(
                name: "Trip",
                newName: "Trips");

            migrationBuilder.RenameTable(
                name: "Stop",
                newName: "Stops");

            migrationBuilder.RenameIndex(
                name: "IX_Stop_TripId",
                table: "Stops",
                newName: "IX_Stops_TripId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stops",
                table: "Stops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Trips_TripId",
                table: "Stops",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Trips_TripId",
                table: "Stops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stops",
                table: "Stops");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Trip");

            migrationBuilder.RenameTable(
                name: "Stops",
                newName: "Stop");

            migrationBuilder.RenameIndex(
                name: "IX_Stops_TripId",
                table: "Stop",
                newName: "IX_Stop_TripId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                table: "Trip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stop",
                table: "Stop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stop_Trip_TripId",
                table: "Stop",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
