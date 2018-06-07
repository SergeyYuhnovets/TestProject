using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ConcertApp.Data.Migrations
{
    public partial class user_experiments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets");


            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tickets");           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {         

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tickets",
                nullable: true);           

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId");
           
        }
    }
}
