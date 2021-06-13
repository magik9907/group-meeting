using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class UpdateMeetingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Meetings",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Meetings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Localisation",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCounter",
                table: "Meetings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserMaxLimit",
                table: "Meetings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Localisation",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UserCounter",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UserMaxLimit",
                table: "Meetings");
        }
    }
}
