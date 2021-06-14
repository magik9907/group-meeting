using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class MeetingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_AspNetUsers_UserId1",
                table: "MeetingUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingUser",
                table: "MeetingUser");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUser_UserId",
                table: "MeetingUser");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUser_UserId1",
                table: "MeetingUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MeetingUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingUser",
                table: "MeetingUser",
                columns: new[] { "UserId", "MeetingID" });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUser_MeetingID",
                table: "MeetingUser",
                column: "MeetingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingUser",
                table: "MeetingUser");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUser_MeetingID",
                table: "MeetingUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "MeetingUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingUser",
                table: "MeetingUser",
                columns: new[] { "MeetingID", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUser_UserId",
                table: "MeetingUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUser_UserId1",
                table: "MeetingUser",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_AspNetUsers_UserId1",
                table: "MeetingUser",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
