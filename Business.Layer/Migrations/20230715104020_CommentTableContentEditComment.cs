using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Layer.Migrations
{
    /// <inheritdoc />
    public partial class CommentTableContentEditComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Comments",
                newName: "Content");
        }
    }
}
