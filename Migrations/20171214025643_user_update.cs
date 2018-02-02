using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AgnosticAlbatros.Migrations
{
    public partial class user_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "UserTitles",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserTitleID",
                table: "UserTitles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTitles_CompanyId",
                table: "UserTitles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTitles_UserTitleID",
                table: "UserTitles",
                column: "UserTitleID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTitles_Companies_CompanyId",
                table: "UserTitles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTitles_UserTitles_UserTitleID",
                table: "UserTitles",
                column: "UserTitleID",
                principalTable: "UserTitles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTitles_Companies_CompanyId",
                table: "UserTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTitles_UserTitles_UserTitleID",
                table: "UserTitles");

            migrationBuilder.DropIndex(
                name: "IX_UserTitles_CompanyId",
                table: "UserTitles");

            migrationBuilder.DropIndex(
                name: "IX_UserTitles_UserTitleID",
                table: "UserTitles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserTitles");

            migrationBuilder.DropColumn(
                name: "UserTitleID",
                table: "UserTitles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "Users");
        }
    }
}
