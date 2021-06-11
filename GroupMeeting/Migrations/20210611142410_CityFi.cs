using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class CityFi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Meetings_MeetingID",
                table: "MeetingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_AspNetUsers_UserId",
                table: "MeetingUser");

            migrationBuilder.DropTable(
                name: "GroupCity");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CityID",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "Groups",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CityID",
                table: "Groups",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Meetings_MeetingID",
                table: "MeetingUser",
                column: "MeetingID",
                principalTable: "Meetings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_AspNetUsers_UserId",
                table: "MeetingUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Meetings_MeetingID",
                table: "MeetingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_AspNetUsers_UserId",
                table: "MeetingUser");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CityID",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GroupCity",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    CityID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCity", x => new { x.GroupID, x.CityID });
                    table.ForeignKey(
                        name: "FK_GroupCity_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupCity_Cities_CityID1",
                        column: x => x.CityID1,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCity_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CityID",
                table: "Groups",
                column: "CityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_CityID",
                table: "GroupCity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_CityID1",
                table: "GroupCity",
                column: "CityID1");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_GroupID",
                table: "GroupCity",
                column: "GroupID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Meetings_MeetingID",
                table: "MeetingUser",
                column: "MeetingID",
                principalTable: "Meetings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_AspNetUsers_UserId",
                table: "MeetingUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
