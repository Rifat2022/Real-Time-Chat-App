using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetsChatApplication.Migrations
{
    /// <inheritdoc />
    public partial class columnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageInfo_UsersInfo_UsersId",
                table: "MessageInfo");

            migrationBuilder.DropIndex(
                name: "IX_MessageInfo_UsersId",
                table: "MessageInfo");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "MessageInfo",
                newName: "SenderId");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "MessageInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "MessageInfo");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "MessageInfo",
                newName: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageInfo_UsersId",
                table: "MessageInfo",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageInfo_UsersInfo_UsersId",
                table: "MessageInfo",
                column: "UsersId",
                principalTable: "UsersInfo",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
