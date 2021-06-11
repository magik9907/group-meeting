using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class Groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupOwner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupOwner",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_GroupID",
                table: "GroupOwner",
                column: "GroupID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_OwnerID",
                table: "GroupOwner",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_UserId",
                table: "GroupOwner",
                column: "UserId");
        }
    }
}
