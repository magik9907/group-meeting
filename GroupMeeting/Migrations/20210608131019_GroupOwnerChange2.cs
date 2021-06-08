using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class GroupOwnerChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupOwner_OwnerID",
                table: "GroupOwner");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_GroupID",
                table: "GroupOwner",
                column: "GroupID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_OwnerID",
                table: "GroupOwner",
                column: "OwnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupOwner_GroupID",
                table: "GroupOwner");

            migrationBuilder.DropIndex(
                name: "IX_GroupOwner_OwnerID",
                table: "GroupOwner");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOwner_OwnerID",
                table: "GroupOwner",
                column: "OwnerID",
                unique: true);
        }
    }
}
