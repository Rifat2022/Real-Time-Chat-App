using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetsChatApplication.Migrations
{
    /// <inheritdoc />
    public partial class colnamechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "MessageInfo",
                newName: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "MessageInfo",
                newName: "Message");
        }
    }
}
