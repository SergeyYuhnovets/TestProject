using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ConcertApp.Data.Migrations
{
    public partial class ForgottenConcertLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcertEventID",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConcertEventID",
                table: "Tickets",
                column: "ConcertEventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ConcertEvent_ConcertEventID",
                table: "Tickets",
                column: "ConcertEventID",
                principalTable: "ConcertEvent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ConcertEvent_ConcertEventID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ConcertEventID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ConcertEventID",
                table: "Tickets");
        }
    }
}
