using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chiai.Server.Migrations
{
    /// <inheritdoc />
    public partial class MessageRatingsAsInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatedPositively",
                table: "ChatMessages");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ChatMessages");

            migrationBuilder.AddColumn<bool>(
                name: "RatedPositively",
                table: "ChatMessages",
                type: "bit",
                nullable: true);
        }
    }
}
