using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Readify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddReturnRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturnRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowedBookId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnRequest_BorrowedBook_BorrowedBookId",
                        column: x => x.BorrowedBookId,
                        principalTable: "BorrowedBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequest_BorrowedBookId",
                table: "ReturnRequest",
                column: "BorrowedBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnRequest");
        }
    }
}
