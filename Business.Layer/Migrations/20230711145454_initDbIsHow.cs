using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Layer.Migrations
{
    /// <inheritdoc />
    public partial class initDbIsHow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "SharedContract",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "SharedContract");

            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Comments");
        }
    }
}
