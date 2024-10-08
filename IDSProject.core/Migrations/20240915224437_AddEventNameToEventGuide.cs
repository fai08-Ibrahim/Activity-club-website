﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDSProject.core.Migrations
{
    /// <inheritdoc />
    public partial class AddEventNameToEventGuide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "EventGuide",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventName",
                table: "EventGuide");
        }
    }
}
