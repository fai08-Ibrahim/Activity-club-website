using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDSProject.core.Migrations
{
    /// <inheritdoc />
    public partial class AddEventNameToEventMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add 'EventName' to 'EventMember' with a default value to prevent issues with existing records.
            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "EventMember",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Set primary key for 'User' if not already set.
            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "id");

            // Add index for 'categoryCode' in 'Event' if it's beneficial for query performance.
            migrationBuilder.CreateIndex(
                name: "IX_Event_categoryCode",
                table: "Event",
                column: "categoryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the added column in 'EventMember'.
            migrationBuilder.DropColumn(
                name: "EventName",
                table: "EventMember");

            // Drop primary key for 'User'.
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            // Remove the index from 'Event'.
            migrationBuilder.DropIndex(
                name: "IX_Event_categoryCode",
                table: "Event");
        }
    }
}
