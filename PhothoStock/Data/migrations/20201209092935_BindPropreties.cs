using Microsoft.EntityFrameworkCore.Migrations;

namespace PhothoStock.data.migrations
{
    public partial class BindPropreties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ApplicationUserId",
                table: "Photos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_ApplicationUserId",
                table: "Photos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_ApplicationUserId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ApplicationUserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Photos");
        }
    }
}
