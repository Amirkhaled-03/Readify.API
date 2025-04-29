using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Readify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBorrowedBookStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BorrowedBook",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BorrowedBook");
        }
    }
}
