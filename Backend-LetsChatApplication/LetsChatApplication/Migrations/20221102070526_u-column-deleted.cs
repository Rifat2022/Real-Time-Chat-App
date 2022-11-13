using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetsChatApplication.Migrations
{
    /// <inheritdoc />
    public partial class ucolumndeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UsersInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "UsersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
