using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twitter.Data.Migrations
{
    public partial class MigEditHashtag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Hashtags",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Hashtags");
        }
    }
}
