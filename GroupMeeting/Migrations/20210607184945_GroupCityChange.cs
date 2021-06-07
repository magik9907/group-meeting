using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class GroupCityChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityID1",
                table: "GroupCity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CityID",
                table: "Groups",
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
                name: "FK_GroupCity_Cities_CityID1",
                table: "GroupCity",
                column: "CityID1",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cities_CityID",
                table: "Groups",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupCity_Cities_CityID1",
                table: "GroupCity");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cities_CityID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CityID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupCity_CityID1",
                table: "GroupCity");

            migrationBuilder.DropIndex(
                name: "IX_GroupCity_GroupID",
                table: "GroupCity");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CityID1",
                table: "GroupCity");
        }
    }
}
