using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twitter.Data.Migrations
{
    public partial class MigEditTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Tags",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Tags");
        }
    }
}
