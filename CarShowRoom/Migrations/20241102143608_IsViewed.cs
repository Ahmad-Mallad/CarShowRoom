using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShowRoom.Migrations
{
    /// <inheritdoc />
    public partial class IsViewed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "ContactSubmissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "ContactSubmissions");
        }
    }
}
