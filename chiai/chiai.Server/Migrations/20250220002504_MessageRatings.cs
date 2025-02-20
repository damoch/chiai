using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chiai.Server.Migrations
{
    /// <inheritdoc />
    public partial class MessageRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RatedPositively",
                table: "ChatMessages",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatedPositively",
                table: "ChatMessages");
        }
    }
}
