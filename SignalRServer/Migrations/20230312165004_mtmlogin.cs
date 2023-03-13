using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoalitionServer.Migrations
{
    public partial class mtmlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "GroupChatsToUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "GroupChatsToUsers");
        }
    }
}
