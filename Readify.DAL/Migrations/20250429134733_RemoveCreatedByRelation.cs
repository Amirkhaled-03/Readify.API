using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Readify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreatedByRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Comment these two lines because the constraint and index don't exist
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_CreatedById",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CreatedById",
                table: "Book");
            */

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Book",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CreatedById",
                table: "Book",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_CreatedById",
                table: "Book",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
