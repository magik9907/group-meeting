using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class GroupCityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupCity_CityID",
                table: "GroupCity");

            migrationBuilder.DropIndex(
                name: "IX_GroupCity_GroupID",
                table: "GroupCity");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_CityID",
                table: "GroupCity",
                column: "CityID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupCity_CityID",
                table: "GroupCity");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_CityID",
                table: "GroupCity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_GroupID",
                table: "GroupCity",
                column: "GroupID",
                unique: true);
        }
    }
}
