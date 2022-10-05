using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twitter.Data.Migrations
{
    public partial class AddMigProfileUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileUsers",
                columns: table => new
                {
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUsers", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_ProfileUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_UserId",
                table: "ProfileUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileUsers");
        }
    }
}
