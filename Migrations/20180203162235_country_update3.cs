using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AgnosticAlbatros.Migrations
{
    public partial class country_update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "PhoneCode",
                table: "Countries",
                newName: "ISOCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISOCode",
                table: "Countries",
                newName: "PhoneCode");

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

            migrationBuilder.AddColumn<string>(
                name: "NumCode",
                table: "Countries",
                nullable: true);
        }
    }
}
