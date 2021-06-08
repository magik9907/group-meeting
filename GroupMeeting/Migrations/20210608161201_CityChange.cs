using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class CityChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_CityID",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CityID",
                table: "Groups",
                column: "CityID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_CityID",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CityID",
                table: "Groups",
                column: "CityID");
        }
    }
}
