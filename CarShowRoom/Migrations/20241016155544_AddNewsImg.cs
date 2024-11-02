using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShowRoom.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsImg",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsImg",
                table: "News");
        }
    }
}
