using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class GroupOwnerAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupCity_Groups_CityID",
                table: "GroupCity");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupCity_Cities_GroupID",
                table: "GroupCity");

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupOwner",
                columns: table => new
                {
                    OwnerID = table.Column<string>(nullable: false),
                    GroupID = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOwner", x => new { x.GroupID, x.OwnerID });
                    table.ForeignKey(
                        name: "FK_GroupOwner_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupOwner_AspNetUsers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupOwner_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    GroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupID, x.UserID });
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OwnerID",
                table: "Groups",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_OwnerID",
                table: "GroupOwner",
                column: "OwnerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_UserId",
                table: "GroupOwner",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserID",
                table: "GroupUser",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupCity_Cities_CityID",
                table: "GroupCity",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupCity_Groups_GroupID",
                table: "GroupCity",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_OwnerID",
                table: "Groups",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupCity_Cities_CityID",
                table: "GroupCity");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupCity_Groups_GroupID",
                table: "GroupCity");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_OwnerID",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupOwner");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropIndex(
                name: "IX_Groups_OwnerID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupCity_Groups_CityID",
                table: "GroupCity",
                column: "CityID",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupCity_Cities_GroupID",
                table: "GroupCity",
                column: "GroupID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
