using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AgnosticAlbatros.Migrations
{
    public partial class country_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "ISO",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISO3",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NiceName",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumCode",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneCode",
                table: "Countries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISO",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ISO3",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "NiceName",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "NumCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "PhoneCode",
                table: "Countries");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Countries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
