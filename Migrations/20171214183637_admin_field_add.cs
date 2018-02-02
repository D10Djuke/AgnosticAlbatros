using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AgnosticAlbatros.Migrations
{
    public partial class admin_field_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ClientID",
                table: "Quotes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ClientID",
                table: "Quotes",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Clients_ClientID",
                table: "Quotes",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Clients_ClientID",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_ClientID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Quotes");
        }
    }
}
