using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Readify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequest_AspNetUsers_UserId",
                table: "BorrowRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequest_Book_BookId",
                table: "BorrowRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequest_AspNetUsers_UserId",
                table: "BorrowRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequest_Book_BookId",
                table: "BorrowRequest",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequest_AspNetUsers_UserId",
                table: "BorrowRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequest_Book_BookId",
                table: "BorrowRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequest_AspNetUsers_UserId",
                table: "BorrowRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequest_Book_BookId",
                table: "BorrowRequest",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
