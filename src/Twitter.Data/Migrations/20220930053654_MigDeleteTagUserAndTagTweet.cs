using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Twitter.Data.Migrations
{
    public partial class MigDeleteTagUserAndTagTweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTweets");

            migrationBuilder.DropTable(
                name: "TagUsers");

            migrationBuilder.RenameColumn(
                name: "TagUserId",
                table: "Tags",
                newName: "TU_Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TweetId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TweetId",
                table: "Tags",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tweets_TweetId",
                table: "Tags",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "TweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Users_UserId",
                table: "Tags",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tweets_TweetId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_UserId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TweetId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_UserId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "TU_Id",
                table: "Tags",
                newName: "TagUserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TweetId",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "TagTweets",
                columns: table => new
                {
                    TT_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TweetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTweets", x => x.TT_Id);
                    table.ForeignKey(
                        name: "FK_TagTweets_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTweets_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagUsers",
                columns: table => new
                {
                    TU_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagUsers", x => x.TU_Id);
                    table.ForeignKey(
                        name: "FK_TagUsers_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTweets_TagId",
                table: "TagTweets",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTweets_TweetId",
                table: "TagTweets",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TagUsers_TagId",
                table: "TagUsers",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagUsers_UserId",
                table: "TagUsers",
                column: "UserId");
        }
    }
}
